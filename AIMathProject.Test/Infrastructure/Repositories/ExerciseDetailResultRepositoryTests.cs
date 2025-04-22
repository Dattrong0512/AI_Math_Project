using AIMathProject.Application.Dto;
using AIMathProject.Application.Dto.ExerciseDetailResultDto;
using AIMathProject.Domain.Entities;
using AIMathProject.Infrastructure.Data;
using AIMathProject.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AIMathProject.Tests.Infrastructure.Repositories
{
    public class ExerciseDetailResultRepositoryTests
    {
        private ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        private ExerciseDetailResultRepository CreateRepository(out ApplicationDbContext context)
        {
            context = CreateDbContext();
            return new ExerciseDetailResultRepository(context);
        }

        //Trường hợp tạo mới ExerciseResult và ExerciseDetailResult khi chưa tồn tại
        [Fact]
        public async Task UpsertExerciseDetailResult_ShouldCreateNewExerciseResultAndExerciseDetailResult_WhenNotExist()
        {
            // Arrange
            var repository = CreateRepository(out var context);

            // Thiết lập dữ liệu giả lập
            var lesson = new Lesson { LessonId = 1,LessonName = "Lesson 1", LessonOrder = 1 };
            var exercise = new Exercise { ExerciseId = 1, ExerciseName = "Ex 1", LessonId = 1 };
            var exerciseDetail1 = new ExerciseDetail { ExerciseDetailId = 1, ExerciseId = 1, QuestionId = 1 };
            var exerciseDetail2 = new ExerciseDetail { ExerciseDetailId = 2, ExerciseId = 1, QuestionId = 2 };
            var enrollment = new Enrollment { EnrollmentId = 1 };

            context.Lessons.Add(lesson);
            context.Exercises.Add(exercise);
            context.ExerciseDetails.AddRange(exerciseDetail1, exerciseDetail2);
            context.Enrollments.Add(enrollment);
            await context.SaveChangesAsync();

            var edrDtoList = new List<ExerciseDetailResultDto>
            {
                new ExerciseDetailResultDto { QuestionId = 1, IsCorrect = true },
                new ExerciseDetailResultDto { QuestionId = 2, IsCorrect = false }
            };

            // Act
            var result = await repository.UpsertExerciseDetailResult(enrollment.EnrollmentId, (int)lesson.LessonOrder, edrDtoList);

            // Assert
            Assert.True(result);

            // Kiểm tra ExerciseResult được tạo mới
            var exerciseResult = await context.ExerciseResults
                .FirstOrDefaultAsync(er => er.EnrollmentId == enrollment.EnrollmentId && er.ExerciseId == exercise.ExerciseId);
            Assert.NotNull(exerciseResult);

            // Kiểm tra ExerciseDetailResult được tạo mới
            var exerciseDetailResults = context.ExerciseDetailResults
                .Where(edr => edr.ExerciseResultId == exerciseResult.ExerciseResultId)
                .ToList();
            Assert.Equal(2, exerciseDetailResults.Count);
            Assert.Contains(exerciseDetailResults, edr => edr.ExerciseDetailId == 1 && edr.IsCorrect == true);
            Assert.Contains(exerciseDetailResults, edr => edr.ExerciseDetailId == 2 && edr.IsCorrect == false);
        }

        //Trường hợp cập nhật ExerciseDetailResult khi đã tồn tại
        [Fact]
        public async Task UpsertExerciseDetailResult_ShouldUpdateExistingExerciseDetailResult_WhenExists()
        {
            // Arrange
            var repository = CreateRepository(out var context);

            // Thiết lập dữ liệu giả lập
            var lesson = new Lesson { LessonId = 1, LessonName = "Lesson 1", LessonOrder = 1 };
            var exercise = new Exercise { ExerciseId = 1, ExerciseName = "Ex 1", LessonId = 1 };
            var exerciseDetail = new ExerciseDetail { ExerciseDetailId = 1, ExerciseId = 1, QuestionId = 1 };
            var enrollment = new Enrollment { EnrollmentId = 1 };
            var exerciseResult = new ExerciseResult { ExerciseResultId = 1, ExerciseId = 1, EnrollmentId = 1 };
            var existingEdr = new ExerciseDetailResult
            {
                ExerciseDetailResultId = 1,
                ExerciseDetailId = 1,
                ExerciseResultId = 1,
                IsCorrect = false // Giá trị ban đầu
            };

            context.Lessons.Add(lesson);
            context.Exercises.Add(exercise);
            context.ExerciseDetails.Add(exerciseDetail);
            context.Enrollments.Add(enrollment);
            context.ExerciseResults.Add(exerciseResult);
            context.ExerciseDetailResults.Add(existingEdr);
            await context.SaveChangesAsync();

            var edrDtoList = new List<ExerciseDetailResultDto>
            {
                new ExerciseDetailResultDto { QuestionId = 1, IsCorrect = true } // Cập nhật IsCorrect thành true
            };

            // Act
            var result = await repository.UpsertExerciseDetailResult(enrollment.EnrollmentId, (int)lesson.LessonOrder, edrDtoList);

            // Assert
            Assert.True(result);

            // Kiểm tra ExerciseDetailResult được cập nhật
            var updatedEdr = await context.ExerciseDetailResults
                .FirstOrDefaultAsync(edr => edr.ExerciseDetailId == exerciseDetail.ExerciseDetailId && edr.ExerciseResultId == exerciseResult.ExerciseResultId);
            Assert.NotNull(updatedEdr);
            Assert.True(updatedEdr.IsCorrect); // Đã được cập nhật từ false thành true
        }

        //Trường hợp ném ngoại lệ khi ExerciseDetail không tồn tại
        [Fact]
        public async Task UpsertExerciseDetailResult_ShouldThrowException_WhenExerciseDetailNotFound()
        {
            // Arrange
            var repository = CreateRepository(out var context);

            // Thiết lập dữ liệu giả lập (không có ExerciseDetail cho QuestionId = 1)
            var lesson = new Lesson { LessonId = 1, LessonName = "Lesson 1", LessonOrder = 1 };
            var exercise = new Exercise { ExerciseId = 1, ExerciseName = "Ex 1", LessonId = 1 };
            var enrollment = new Enrollment { EnrollmentId = 1 };

            context.Lessons.Add(lesson);
            context.Exercises.Add(exercise);
            context.Enrollments.Add(enrollment);
            await context.SaveChangesAsync();

            var edrDtoList = new List<ExerciseDetailResultDto>
            {
                new ExerciseDetailResultDto { QuestionId = 1, IsCorrect = true }
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                repository.UpsertExerciseDetailResult(enrollment.EnrollmentId, (int)lesson.LessonOrder, edrDtoList));
            Assert.Equal("ExerciseDetail not found for QuestionId: 1", exception.Message);
        }
    }
}