using AIMathProject.Application.Dto;
using AIMathProject.Application.Dto.ExerciseResultDto;
using AIMathProject.Domain.Entities;
using AIMathProject.Infrastructure.Data;
using AIMathProject.Infrastructure.Repositories;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AIMathProject.Tests.Infrastructure.Repositories
{
    public class ExerciseResultRepositoryTests
    {
        private ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        private ExerciseResultRepository CreateRepository(out ApplicationDbContext context)
        {
            context = CreateDbContext();
            return new ExerciseResultRepository(context);
        }

        //Trường hợp trả về ExerciseResultDto thành công khi tồn tại
        [Fact]
        public async Task GetDetailExerciseResultById_ShouldReturnExerciseResultDto_WhenExists()
        {
            // Arrange
            var repository = CreateRepository(out var context);

            // Thiết lập dữ liệu giả lập
            var lesson = new Lesson { LessonId = 1, LessonName = "Lesson 1", LessonOrder = 1 };
            var exercise = new Exercise { ExerciseId = 1, ExerciseName = "Ex 1",  LessonId = 1 };
            var enrollment = new Enrollment { EnrollmentId = 1 };
            var exerciseResult = new ExerciseResult
            {
                ExerciseResultId = 1,
                ExerciseId = 1,
                EnrollmentId = 1,
                Score = 80,
                DoneAt = DateTime.UtcNow
            };
            var question1 = new Question
            {
                QuestionId = 1,
                QuestionType = "multiple_choice",
                Difficulty = 1,
                LessonId = 1,
                QuestionContent = "What is 2+2?",
                ImgUrl = null,
                PdfSolution = null
            };
            var question2 = new Question
            {
                QuestionId = 2,
                QuestionType = "fill_in_blank",
                Difficulty = 2,
                LessonId = 1,
                QuestionContent = "Fill in: 1 + _ = 3",
                ImgUrl = null,
                PdfSolution = null
            };
            var exerciseDetail1 = new ExerciseDetail { ExerciseDetailId = 1, ExerciseId = 1, QuestionId = 1 };
            var exerciseDetail2 = new ExerciseDetail { ExerciseDetailId = 2, ExerciseId = 1, QuestionId = 2 };
            var exerciseDetailResult1 = new ExerciseDetailResult
            {
                ExerciseDetailResultId = 1,
                ExerciseResultId = 1,
                ExerciseDetailId = 1,
                IsCorrect = true
            };
            var exerciseDetailResult2 = new ExerciseDetailResult
            {
                ExerciseDetailResultId = 2,
                ExerciseResultId = 1,
                ExerciseDetailId = 2,
                IsCorrect = false
            };
            var choiceAnswer1 = new ChoiceAnswer
            {
                AnswerId = 1,
                QuestionId = 1,
                Content = "4",
                IsCorrect = true,
                ImgUrl = null
            };
            var choiceAnswer2 = new ChoiceAnswer
            {
                AnswerId = 2,
                QuestionId = 1,
                Content = "5",
                IsCorrect = false,
                ImgUrl = null
            };
            var fillAnswer = new FillAnswer
            {
                AnswerId = 1,
                QuestionId = 2,
                CorrectAnswer = "2"
            };

            context.Lessons.Add(lesson);
            context.Exercises.Add(exercise);
            context.Enrollments.Add(enrollment);
            context.ExerciseResults.Add(exerciseResult);
            context.Questions.AddRange(question1, question2);
            context.ExerciseDetails.AddRange(exerciseDetail1, exerciseDetail2);
            context.ExerciseDetailResults.AddRange(exerciseDetailResult1, exerciseDetailResult2);
            context.ChoiceAnswers.AddRange(choiceAnswer1, choiceAnswer2);
            context.FillAnswers.Add(fillAnswer);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetDetailExerciseResultById(enrollment.EnrollmentId, (int)lesson.LessonOrder);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(exerciseResult.ExerciseId, result.ExerciseId);
            Assert.Equal(enrollment.EnrollmentId, result.EnrollmentId);
            Assert.Equal(exerciseResult.Score, result.Score);
            Assert.Equal(exerciseResult.DoneAt, result.DoneAt);

            // Kiểm tra ExerciseDetailResults
            Assert.Equal(2, result.ExerciseDetailResults.Count);
            var edrDto1 = result.ExerciseDetailResults.FirstOrDefault(edr => edr.ExerciseDetailId == 1);
            var edrDto2 = result.ExerciseDetailResults.FirstOrDefault(edr => edr.ExerciseDetailId == 2);

            Assert.NotNull(edrDto1);
            Assert.True(edrDto1.IsCorrect);
            Assert.NotNull(edrDto1.ExerciseDetail);
            Assert.NotNull(edrDto1.ExerciseDetail.Question);
            Assert.Equal("multiple_choice", edrDto1.ExerciseDetail.Question.QuestionType);
            Assert.Equal(2, edrDto1.ExerciseDetail.Question.ChoiceAnswers.Count);
            Assert.Contains(edrDto1.ExerciseDetail.Question.ChoiceAnswers, ca => ca.Content == "4" && ca.IsCorrect.GetValueOrDefault());

            Assert.NotNull(edrDto2);
            Assert.False(edrDto2.IsCorrect);
            Assert.NotNull(edrDto2.ExerciseDetail);
            Assert.NotNull(edrDto2.ExerciseDetail.Question);
            Assert.Equal("fill_in_blank", edrDto2.ExerciseDetail.Question.QuestionType);
            Assert.Single(edrDto2.ExerciseDetail.Question.FillAnswers);
            Assert.Contains(edrDto2.ExerciseDetail.Question.FillAnswers, fa => fa.CorrectAnswer == "2");
        }

        //Trường hợp ném ngoại lệ khi ExerciseResult không tồn tại
        [Fact]
        public async Task GetDetailExerciseResultById_ShouldThrowException_WhenExerciseResultNotFound()
        {
            // Arrange
            var repository = CreateRepository(out var context);

            // Thiết lập dữ liệu giả lập (không có ExerciseResult)
            var lesson = new Lesson { LessonId = 1,LessonName = "Lesson 1", LessonOrder = 1 };
            var exercise = new Exercise { ExerciseId = 1, ExerciseName = "Ex 1", LessonId = 1 };
            var enrollment = new Enrollment { EnrollmentId = 1 };

            context.Lessons.Add(lesson);
            context.Exercises.Add(exercise);
            context.Enrollments.Add(enrollment);
            await context.SaveChangesAsync();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                repository.GetDetailExerciseResultById(enrollment.EnrollmentId, (int)lesson.LessonOrder));
            Assert.Equal("ExerciseResult not found.", exception.Message);
        }

        //Trường hợp ném ngoại lệ khi ExerciseDetail không tồn tại
        [Fact]
        public async Task GetDetailExerciseResultById_ShouldThrowException_WhenExerciseDetailNotFound()
        {
            // Arrange
            var repository = CreateRepository(out var context);

            // Thiết lập dữ liệu giả lập (có ExerciseResult nhưng thiếu ExerciseDetail)
            var lesson = new Lesson { LessonId = 1, LessonName = "Lesson 1", LessonOrder = 1 };
            var exercise = new Exercise { ExerciseId = 1, ExerciseName = "Ex 1", LessonId = 1 };
            var enrollment = new Enrollment { EnrollmentId = 1 };
            var exerciseResult = new ExerciseResult
            {
                ExerciseResultId = 1,
                ExerciseId = 1,
                EnrollmentId = 1
            };
            var exerciseDetailResult = new ExerciseDetailResult
            {
                ExerciseDetailResultId = 1,
                ExerciseResultId = 1,
                ExerciseDetailId = 999 // ExerciseDetailId không tồn tại
            };

            context.Lessons.Add(lesson);
            context.Exercises.Add(exercise);
            context.Enrollments.Add(enrollment);
            context.ExerciseResults.Add(exerciseResult);
            context.ExerciseDetailResults.Add(exerciseDetailResult);
            await context.SaveChangesAsync();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                repository.GetDetailExerciseResultById(enrollment.EnrollmentId, (int)lesson.LessonOrder));
            Assert.Equal("ExerciseDetail not found for ExerciseDetailId: 999", exception.Message);
        }
    }
}