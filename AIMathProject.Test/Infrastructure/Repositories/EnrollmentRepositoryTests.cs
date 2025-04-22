using AIMathProject.Application.Dto.EnrollmentDto;
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
    public class EnrollmentRepositoryTests
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepositoryTests()
        {
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

            _context.Enrollments.AddRange(
                new Enrollment { EnrollmentId = 1, UserId = 1, EnrollmentDate = new DateOnly(2023, 1, 1) },
                new Enrollment { EnrollmentId = 2, UserId = 1, EnrollmentDate = new DateOnly(2023, 2, 1) },
                new Enrollment { EnrollmentId = 3, UserId = 2, EnrollmentDate = new DateOnly(2023, 3, 1) }
            );

            _context.SaveChanges();
        }

        // Trường hợp trả về danh sách Enrollment cho người dùng tồn tại
        [Fact]
        public async Task GetAllEnrollmentByID_ShouldReturnEnrollmentsForUser()
        {
            // Arrange
            var repository = new EnrollmentRepository(_context);

            // Act
            var result = await repository.GetAllEnrollmentByID(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, enrollment => Assert.Equal(1, enrollment.UserId));
        }

        // Trường hợp trả về danh sách rỗng khi không tìm thấy người dùng
        [Fact]
        public async Task GetAllEnrollmentByID_ShouldReturnEmptyListForNonExistentUser()
        {
            // Arrange
            var repository = new EnrollmentRepository(_context);

            // Act
            var result = await repository.GetAllEnrollmentByID(99);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
