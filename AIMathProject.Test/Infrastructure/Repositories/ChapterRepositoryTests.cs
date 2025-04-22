using AIMathProject.Application.Dto;
using AIMathProject.Domain.Entities;
using AIMathProject.Infrastructure.Data;
using AIMathProject.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AIMathProject.Tests.Repositories
{
    public class ChapterRepositoryTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly ApplicationDbContext _context;

        public ChapterRepositoryTests()
        {
            _mapperMock = new Mock<IMapper>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);

            // Seed the in-memory database with test data
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Chapters.AddRange(
                new Chapter { ChapterId = 1, Grade = 1, ChapterOrder = 1, ChapterName = "Chapter 1", Semester = 1 },
                new Chapter { ChapterId = 2, Grade = 2, ChapterOrder = 2, ChapterName = "Chapter 2", Semester = 2 }
            );

            _context.Lessons.AddRange(
                new Lesson { ChapterId = 1, LessonOrder = 1, LessonName = "Lesson 1", LessonPdfUrl = "Content 1", LessonVideoUrl = "Content 3" },
                new Lesson { ChapterId = 2, LessonOrder = 2, LessonName = "Lesson 2", LessonPdfUrl = "Content 2", LessonVideoUrl = "Content 4" }
            );

            _context.SaveChanges();
        }

        //Trường hợp trả về danh sách tất cả các chương
        [Fact]
        public async Task GetAllChapters_ShouldReturnAllChapters()
        {
            // Arrange
            var repository = new ChapterRepository(_context, _mapperMock.Object);

            // Act
            var result = await repository.GetAllChapters();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        //Trường hợp trả về danh sách tất cả các chương chi tiết
        [Fact]
        public async Task GetAllDetailChapters_ShouldReturnAllDetailChapters()
        {
            // Arrange
            var repository = new ChapterRepository(_context, _mapperMock.Object);

            // Act
            var result = await repository.GetAllDetailChapters();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, chapter => Assert.NotNull(chapter.Lessons));
        }

        //Trường hợp trả về danh sách các chương chi tiết theo lớp
        [Fact]
        public async Task GetAllDetailChaptersClassified_ShouldReturnChaptersByGrade()
        {
            // Arrange
            var repository = new ChapterRepository(_context, _mapperMock.Object);
            int grade = 1;

            // Act
            var result = await repository.GetAllDetailChaptersClassified(grade);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.All(result, chapter => Assert.Equal((short)grade, chapter.Grade));
        }
    }
}