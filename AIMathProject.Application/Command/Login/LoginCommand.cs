using AIMathProject.Application.Abstracts;
using AIMathProject.Domain.Exceptions;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMathProject.Domain.Interfaces;

namespace AIMathProject.Application.Command.Login
{
    public class LoginCommand : IRequest<(string jwtToken, string refreshToken)>
    {
        public LoginRequest LoginRequest { get; set; }

        public LoginCommand(LoginRequest loginRequest)
        {
            LoginRequest = loginRequest;
        }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, (string jwtToken, string refreshToken)>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthTokenProcessor _authTokenProcessor;
        private readonly IUserStatisticsRepository _userStatisticsRepository;
        public LoginCommandHandler(UserManager<User> userManager, IAuthTokenProcessor authTokenProcessor, IUserStatisticsRepository userStatisticsRepository)
        {
            _userManager = userManager;
            _authTokenProcessor = authTokenProcessor;
            _userStatisticsRepository = userStatisticsRepository;
        }

        public async Task<(string jwtToken, string refreshToken)> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.LoginRequest.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.LoginRequest.Password))
            {
                throw new LoginFailedException(request.LoginRequest.Email);
            }

            var (jwtToken, expirationDateInUtc) = await _authTokenProcessor.GenerateJwtToken(user);
            if(jwtToken == null)
            {
                throw new EmailNotConfirmException(user.Email);
            }
            var refreshToken = _authTokenProcessor.GenerateRefreshToken();
            var refreshTokenExpirationDateInUtc = DateTime.UtcNow.AddDays(90);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiredAtUtc = refreshTokenExpirationDateInUtc;
            await _userManager.UpdateAsync(user);

            _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
            _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", refreshToken, refreshTokenExpirationDateInUtc);

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Admin"))
            {
                await _userStatisticsRepository.StartUserSession(user.Id);
            }

            return (jwtToken, refreshToken);
        }
    }
}
