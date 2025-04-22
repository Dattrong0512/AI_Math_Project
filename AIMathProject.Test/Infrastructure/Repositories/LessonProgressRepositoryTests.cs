using AIMathProject.Domain.Entities;
using AIMathProject.Infrastructure.Data;
using AIMathProject.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AIMathProject.Tests.Infrastructure.Repositories
{
    public class LessonProgressRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly LessonProgressRepository _repository;
        private readonly ILogger<LessonProgressRepository> _logger;
        public LessonProgressRepositoryTests()
        {
            // Sử dụng SQLite trong chế độ bộ nhớ
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection).Options;

            _context = new ApplicationDbContext(options);
            var mockLogger = new Mock<ILogger<LessonProgressRepository>>();
            _logger = mockLogger.Object;
            _repository = new LessonProgressRepository(_context, _logger);

            // Seed the in-memory database with test data
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Users.AddRange(
                new User { Id = 1, UserName = "user1", Email = "user1@example.com" },
                new User { Id = 2, UserName = "user2", Email = "user2@example.com" }
            );

            _context.Enrollments.AddRange(
                new Enrollment { EnrollmentId = 1, UserId = 1, Semester = 1 },
                new Enrollment { EnrollmentId = 2, UserId = 1, Semester = 2 },
                new Enrollment { EnrollmentId = 3, UserId = 2, Semester = 1 }
            );

            _context.Lessons.AddRange(
                new Lesson { LessonId = 1, LessonOrder = 1, LessonName = "Lesson 1", LessonPdfUrl = "Content 1", LessonVideoUrl = "Content 2" },
                new Lesson { LessonId = 2, LessonOrder = 2, LessonName = "Lesson 2", LessonPdfUrl = "Content 3", LessonVideoUrl = "Content 4" }
            );

            _context.LessonProgresses.AddRange(
                new LessonProgress { LearningProgressId = 1, EnrollmentId = 1, LessonId = 1, Status = "in_progress"},
                new LessonProgress { LearningProgressId = 2, EnrollmentId = 3, LessonId = 2, Status = "completed" },
                new LessonProgress { LearningProgressId = 3, EnrollmentId = 2, LessonId = 1, Status = "completed" }
            );

            _context.SaveChanges();
        }

        //Trường hợp trả về danh sách LessonProgress cho người dùng
        [Fact]
        public async Task GetAllInfLessonProgress_ShouldReturnLessonProgressForUser()
        {
            // Act
            var result = await _repository.GetAllInfLessonProgress(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, lp => Assert.Equal((short)1, lp.Lesson?.LessonOrder));
        }

        //Trường hợp trả về danh sách LessonProgress theo người dùng và học kỳ
        [Fact]
        public async Task GetAllInfLessonProgressClassified_ShouldReturnLessonProgressForUserAndSemester()
        {
            // Act
            var result = await _repository.GetAllInfLessonProgressClassified(1, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal((short)1, result.First().Lesson?.LessonOrder);
        }


        //Trường hợp trả về một LessonProgress theo ID
        [Fact]
        public async Task GetInfoOneLessonProgress_ShouldReturnLessonProgress()
        {
            // Act
            var result = await _repository.GetInfoOneLessonProgress(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.LearningProgressId);
            Assert.Equal("in_progress", result.Status);
        }

        //Trường hợp trả về null khi không tìm thấy LessonProgress
        [Fact]
        public async Task GetInfoOneLessonProgress_ShouldReturnNullForNonExistentId()
        {
            // Act
            var result = await _repository.GetInfoOneLessonProgress(99);

            // Assert
            Assert.Null(result);
        }


        //Trường hợp cập nhật LessonProgress thành công
        [Fact]
        public async Task UpdateLearningProgress_ShouldUpdateLearningProgress()
        {
            // Act
            var result = await _repository.UpdateLearningProgress(1, "completed");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("completed", result.Status);
        }

        //Trường hợp trả về null khi không tìm thấy LessonProgress
        [Fact]
        public async Task UpdateLearningProgress_ShouldReturnNullForNonExistentId()
        {
            // Act
            var result = await _repository.UpdateLearningProgress(99, "in_progress");

            // Assert
            Assert.Null(result);
        }
    }
}
