using AIMathProject.Application.Abstracts;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.Login
{
    public class LogoutCommand : IRequest<Unit>
    {
        public int UserId { get; set; }

        public LogoutCommand(int userId)
        {
            UserId = userId;
        }
    }

    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthTokenProcessor _authTokenProcessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStatisticsRepository _userStatisticsRepository;
        public LogoutCommandHandler(
            UserManager<User> userManager,
            IAuthTokenProcessor authTokenProcessor,
            IHttpContextAccessor httpContextAccessor,
            IStatisticsRepository userStatisticsRepository)
        {
            _userManager = userManager;
            _authTokenProcessor = authTokenProcessor;
            _httpContextAccessor = httpContextAccessor;
            _userStatisticsRepository = userStatisticsRepository;
        }
        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user != null)
            {
                // Xóa refresh token từ user
                user.RefreshToken = null;
                user.RefreshTokenExpiredAtUtc = null;
                await _userManager.UpdateAsync(user);

                // Xóa cookies
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("REFRESH_TOKEN");
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("ACCESS_TOKEN");

                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains("Admin"))
                {
                    await _userStatisticsRepository.EndUserSession(user.Id);
                }
                //await _httpContextAccessor.HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            }

            return Unit.Value;
        }
    }
}