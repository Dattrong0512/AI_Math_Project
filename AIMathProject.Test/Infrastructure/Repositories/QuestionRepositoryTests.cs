using AIMathProject.Application.Dto.QuestionDto;
using AIMathProject.Domain.Entities;
using AIMathProject.Infrastructure.Data;
using AIMathProject.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AIMathProject.Tests.Infrastructure.Repositories
{
    public class QuestionRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly QuestionRepository _repository;

        public QuestionRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new QuestionRepository(_context);

            // Seed the in-memory database with test data
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Chapters.AddRange(
                new Chapter { ChapterId = 1, ChapterName = "Chapter 1", Grade = 1, ChapterOrder = 1 },
                new Chapter { ChapterId = 2, ChapterName = "Chapter 2", Grade = 1, ChapterOrder = 2 }
            );

            _context.Lessons.AddRange(
                new Lesson { LessonId = 1, LessonOrder = 1, LessonName = "Lesson 1", LessonVideoUrl = "Content 1", LessonPdfUrl = "Content 3", ChapterId = 1 },
                new Lesson { LessonId = 2, LessonOrder = 2, LessonName = "Lesson 2", LessonVideoUrl = "Content 2", LessonPdfUrl = "Content 4", ChapterId = 1 }
            );

            _context.Exercises.AddRange(
                new Exercise { ExerciseId = 1, ExerciseName = "ex 1", LessonId = 1 },
                new Exercise { ExerciseId = 2, ExerciseName = "ex 2", LessonId = 2 }
            );

            _context.ExerciseDetails.AddRange(
                new ExerciseDetail { ExerciseId = 1, QuestionId = 1 },
                new ExerciseDetail { ExerciseId = 1, QuestionId = 2 }
            );

            _context.Questions.AddRange(
                new Question { QuestionId = 1, LessonId = 1, QuestionType = "multiple_choice" },
                new Question { QuestionId = 2, LessonId = 1, QuestionType = "matching" }
            );

            _context.ChoiceAnswers.AddRange(
                new ChoiceAnswer { AnswerId = 1, ImgUrl = "url 1", QuestionId = 1 },
                new ChoiceAnswer { AnswerId = 2, ImgUrl = "url 2", QuestionId = 1 }
            );

            _context.MatchingAnswers.AddRange(
                new MatchingAnswer { AnswerId = 1, CorrectAnswer = "hehe", ImgUrl = "url 3", QuestionId = 2 },
                new MatchingAnswer { AnswerId = 2, CorrectAnswer = "hihi", ImgUrl = "url 4", QuestionId = 2 }
            );

            _context.SaveChanges();
        }

        //Trường hợp trả về danh sách câu hỏi theo LessonId
        [Fact]
        public async Task GetAllQuestionByLessonID_ShouldReturnQuestions()
        {
            // Arrange
            int grade = 1;
            int lessonOrder = 1;

            // Act
            var result = await _repository.GetAllQuestionByLessonID(grade, lessonOrder);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, q => q.QuestionType == "multiple_choice");
            Assert.Contains(result, q => q.QuestionType == "matching");
        }
    }
}
