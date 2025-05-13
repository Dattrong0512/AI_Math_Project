using AIMathProject.Application.Abstracts;
using AIMathProject.Application.Dto;
using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Application.Dto.UserDto;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Exceptions;
using AIMathProject.Infrastructure.Data;
using AIMathProject.Infrastructure.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace AIMathProject.Tests.Infrastructure.Repositories
{
    public class UserRepositoryTests
    {
        private UserRepository CreateUserRepository(
            out ApplicationDbContext context,
            out Mock<IMapper> mockMapper,
            out Mock<IHttpContextAccessor> mockHttpContextAccessor,
            out Mock<LinkGenerator> mockLinkGenerator,
            out Mock<UserManager<User>> mockUserManager,
            out Mock<ITemplateReader> mockEmailTemplateReader,
            out Mock<IEmailHelper> mockEmailHelper)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);
            mockMapper = new Mock<IMapper>();
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockLinkGenerator = new Mock<LinkGenerator>();
            mockUserManager = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                Mock.Of<IOptions<IdentityOptions>>(),
                Mock.Of<IPasswordHasher<User>>(),
                Array.Empty<IUserValidator<User>>(),
                Array.Empty<IPasswordValidator<User>>(),
                Mock.Of<ILookupNormalizer>(),
                Mock.Of<IdentityErrorDescriber>(),
                Mock.Of<IServiceProvider>(),
                Mock.Of<ILogger<UserManager<User>>>()
            );
            mockEmailTemplateReader = new Mock<ITemplateReader>();
            mockEmailHelper = new Mock<IEmailHelper>();

            return new UserRepository(
                context,
                mockMapper.Object,
                mockHttpContextAccessor.Object,
                mockLinkGenerator.Object,
                mockUserManager.Object,
                mockEmailTemplateReader.Object,
                mockEmailHelper.Object
            );
        }
        //Trường hợp trả về danh sách người dùng phân trang
        [Fact]
        public async Task GetInfoUser_ShouldReturnPaginatedUsers()
        {
            // Arrange
            var userRepository = CreateUserRepository(
                out var context,
                out var mockMapper,
                out var mockHttpContextAccessor,
                out var mockLinkGenerator,
                out var mockUserManager,
                out var mockEmailTemplateReader,
                out var mockEmailHelper);

            // Thêm dữ liệu vào InMemory database
            context.Users.AddRange(new List<User>
            {
                new User { Id = 1, UserName = "User1" },
                new User { Id = 2, UserName = "User2" }
            });
            await context.SaveChangesAsync();

            mockMapper.Setup(m => m.Map<List<UserDto>>(It.IsAny<List<User>>()))
                .Returns(new List<UserDto>
                {
                    new UserDto { UserId = 1, UserName = "User1" },
                    new UserDto { UserId = 2, UserName = "User2" }
                });

            // Act
            var result = await userRepository.GetInfoUser(0, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Items.Count);
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(0, result.PageIndex);
            Assert.Equal(2, result.PageSize);
            Assert.Contains(result.Items, u => u.UserName == "User1");
            Assert.Contains(result.Items, u => u.UserName == "User2");
        }

        //Trường hợp trả về danh sách người dùng phân trang rỗng khi không có người dùng
        [Fact]
        public async Task GetInfoUser_ShouldReturnEmptyPaginatedUsers_WhenNoUsers()
        {
            // Arrange
            var userRepository = CreateUserRepository(
                out var context,
                out var mockMapper,
                out var mockHttpContextAccessor,
                out var mockLinkGenerator,
                out var mockUserManager,
                out var mockEmailTemplateReader,
                out var mockEmailHelper);

            // Không thêm dữ liệu vào context.Users để mô phỏng trường hợp không có người dùng

            mockMapper.Setup(m => m.Map<List<UserDto>>(It.Is<List<User>>(list => list.Count == 0)))
                .Returns(new List<UserDto>());

            // Act
            var result = await userRepository.GetInfoUser(0, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Items); 
            Assert.Equal(0, result.TotalCount); 
            Assert.Equal(0, result.PageIndex);
            Assert.Equal(2, result.PageSize);
        }

        //Trường hợp ném UnauthorizedAccessException khi không có Claim UserId
        [Fact]
        public async Task GetInfoUserLogin_ShouldThrowUnauthorizedAccessException_WhenUserIdClaimIsMissing()
        {
            // Arrange
            var userRepository = CreateUserRepository(
                out var context,
                out var mockMapper,
                out var mockHttpContextAccessor,
                out var mockLinkGenerator,
                out var mockUserManager,
                out var mockEmailTemplateReader,
                out var mockEmailHelper);

            mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(new DefaultHttpContext());

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => userRepository.GetInfoUserLogin());
        }

        //Trường hợp trả về người dùng khi RefreshToken tồn tại
        [Fact]
        public async Task GetUserByRefreshTokenAsync_ShouldReturnUser_WhenRefreshTokenExists()
        {
            // Arrange
            var userRepository = CreateUserRepository(
                out var context,
                out var mockMapper,
                out var mockHttpContextAccessor,
                out var mockLinkGenerator,
                out var mockUserManager,
                out var mockEmailTemplateReader,
                out var mockEmailHelper);

            var user = new User { Id = 1, RefreshToken = "valid_token" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Act
            var result = await userRepository.GetUserByRefreshTokenAsync("valid_token");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        //Trường hợp gửi email xác nhận thành công
        [Fact]
        public async Task SendEmailConfirm_ShouldSendEmailSuccessfully()
        {
            // Arrange
            var userRepository = CreateUserRepository(
                out var context,
                out var mockMapper,
                out var mockHttpContextAccessor,
                out var mockLinkGenerator,
                out var mockUserManager,
                out var mockEmailTemplateReader,
                out var mockEmailHelper);

            var user = new User { Id = 1, UserName = "TestUser", Email = "test@example.com" };
            var token = "email_token";
            var confirmUrl = "http://example.com/confirm";
            var cancellationToken = CancellationToken.None;

            // Mock HttpContext và Request.Scheme
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "https";
            mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(httpContext);

            // Mock GenerateEmailConfirmationTokenAsync
            mockUserManager.Setup(u => u.GenerateEmailConfirmationTokenAsync(user))
                .ReturnsAsync(token);

            // Mock GetUriByAddress (phương thức gốc mà GetUriByAction gọi)
            mockLinkGenerator.Setup(l => l.GetUriByAddress(
                It.IsAny<HttpContext>(),
                It.IsAny<RouteValuesAddress>(),
                It.IsAny<RouteValueDictionary>(),
                It.IsAny<RouteValueDictionary>(),
                It.IsAny<string>(),
                It.IsAny<HostString?>(),
                It.IsAny<PathString?>(),
                It.IsAny<FragmentString>(),
                It.IsAny<LinkOptions?>()))
            .Returns(confirmUrl);

            // Mock GetTemplate
            var emailTemplate = "<html>Hello {0}, please confirm your email: {1}</html>";
            mockEmailTemplateReader.Setup(e => e.GetTemplate("Template/ConfirmEmail.html"))
                .ReturnsAsync(emailTemplate);

            // Mock SendEmailAsync và lưu tham số EmailRequest để kiểm tra
            EmailRequest capturedRequest = null;
            mockEmailHelper.Setup(e => e.SendEmailAsync(It.IsAny<EmailRequest>(), It.IsAny<CancellationToken>()))
                .Callback<EmailRequest, CancellationToken>((request, ct) =>
                {
                    capturedRequest = request;
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await userRepository.SendEmailConfirm(user, cancellationToken);

            // Assert
            Assert.Equal(Unit.Value, result);

            // Kiểm tra tham số EmailRequest được truyền vào SendEmailAsync
            mockEmailHelper.Verify(e => e.SendEmailAsync(It.IsAny<EmailRequest>(), cancellationToken), Times.Once());
            Assert.NotNull(capturedRequest);
            Assert.Equal(user.Email, capturedRequest.To);
            Assert.Equal("Confirm Email for Register", capturedRequest.Subject);
            Assert.Equal(string.Format(emailTemplate, user.UserName, confirmUrl), capturedRequest.Content);
        }

        //Trường hợp ném NullReferenceException khi HttpContext là null
        [Fact]
        public async Task SendEmailConfirm_ShouldThrowNullReferenceException_WhenHttpContextIsNull()
        {
            // Arrange
            var userRepository = CreateUserRepository(
                out var context,
                out var mockMapper,
                out var mockHttpContextAccessor,
                out var mockLinkGenerator,
                out var mockUserManager,
                out var mockEmailTemplateReader,
                out var mockEmailHelper);

            var user = new User { Id = 1, UserName = "TestUser", Email = "test@example.com" };
            var cancellationToken = CancellationToken.None;

            // Mock HttpContext trả về null
            mockHttpContextAccessor.Setup(h => h.HttpContext).Returns((HttpContext)null);

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => userRepository.SendEmailConfirm(user, cancellationToken));
        }
    }
}