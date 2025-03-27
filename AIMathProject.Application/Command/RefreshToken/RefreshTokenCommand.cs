using AIMathProject.Application.Abstracts;
using AIMathProject.Domain.Exceptions;
using AIMathProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.RefreshToken
{
    public class RefreshTokenCommand : IRequest<(string jwtToken, string refreshToken)>
    {
        public string RefreshToken { get; set; }

        public RefreshTokenCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, (string jwtToken, string refreshToken)>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IAuthTokenProcessor _authTokenProcessor;

        public RefreshTokenCommandHandler(IUserRepository userRepository, UserManager<User> userManager, IAuthTokenProcessor authTokenProcessor)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _authTokenProcessor = authTokenProcessor;
        }

        public async Task<(string jwtToken, string refreshToken)> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                throw new RefreshTokenException("Refresh token is missing.");
            }

            var user = await _userRepository.GetUserByRefreshTokenAsync(request.RefreshToken);
            if (user == null)
            {
                throw new RefreshTokenException("Unable to retrieve user for refresh token");
            }

            if (user.RefreshTokenExpiredAtUtc < DateTime.UtcNow)
            {
                throw new RefreshTokenException("Refresh token is expired.");
            }

            var (jwtToken, expirationDateInUtc) = await _authTokenProcessor.GenerateJwtToken(user);
            var refreshToken = _authTokenProcessor.GenerateRefreshToken();
            var refreshTokenExpirationDateInUtc = DateTime.UtcNow.AddDays(90);


            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiredAtUtc = refreshTokenExpirationDateInUtc;
            await _userManager.UpdateAsync(user);

            _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
            _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", refreshToken, refreshTokenExpirationDateInUtc);

            return (jwtToken, refreshToken);
        }
    }
}
