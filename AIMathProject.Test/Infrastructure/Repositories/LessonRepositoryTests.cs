using AIMathProject.Application.Dto;
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
    public class LessonRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly LessonRepository _repository;

        public LessonRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new LessonRepository(_context);

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

        //Trường hợp tạo mới một Lesson thành công
        [Fact]
        public async Task CreateLesson_ShouldCreateLesson()
        {
            // Arrange
            int grade = 1;
            int chapterOrder = 1;
            var lessonDto = new LessonDto
            {
                LessonOrder = 3,
                LessonName = "Lesson 3",
                LessonVideoUrl = "Content 5",
                LessonPdfUrl = "Content 6"
            };

            // Act
            var result = await _repository.CreateLesson(grade, chapterOrder, lessonDto);

            // Assert
            Assert.True(result);
            var createdLesson = await _context.Lessons.FirstOrDefaultAsync(l => l.LessonName == "Lesson 3");
            Assert.NotNull(createdLesson);
            Assert.Equal((short)3, createdLesson.LessonOrder);
        }

        //Trường hợp trả về chi tiết Lesson theo grade và lessonOrder
        [Fact]
        public async Task GetDetailLessonById_ShouldReturnLessonDetails()
        {
            // Arrange
            int grade = 1;
            int lessonOrder = 1;

            // Act
            var result = await _repository.GetDetailLessonById(grade, lessonOrder);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Lesson 1", result.LessonName);
            Assert.Equal(2, result.Questions?.Count);
        }

        //Trường hợp trả về danh sách Lesson theo grade và lessonName
        [Fact]
        public async Task GetDetailLessonByName_ShouldReturnFilteredLessons()
        {
            // Arrange
            int grade = 1;
            string lessonName = "Lesson 1";

            // Act
            var result = await _repository.GetDetailLessonByName(grade, lessonName);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Lesson 1", result.First().LessonName);
        }
    }
}
