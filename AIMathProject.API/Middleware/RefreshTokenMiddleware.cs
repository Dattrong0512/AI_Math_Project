using AIMathProject.Application.Command.RefreshToken;
using AIMathProject.Application.Dto.LoginDto;
using AIMathProject.Domain.Exceptions;
using AIMathProject.Infrastructure.Options;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace AIMathProject.API.Middleware
{
    public class RefreshTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RefreshTokenMiddleware> _logger;
        private readonly string _loginAccount;
        private readonly IServiceProvider _serviceProvider;

        public RefreshTokenMiddleware(
            RequestDelegate next,
            IConfiguration configuration,
            ILogger<RefreshTokenMiddleware> logger,
            IServiceProvider serviceProvider,
            string loginAccount = "/account"
            )
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _loginAccount = loginAccount;
        }

        public async Task Invoke(HttpContext context)
        {

            _logger.LogInformation($"Information request: {context.Request.Path}");
            var path = context.Request.Path.Value; // Lấy đường dẫn dưới dạng chuỗi
            if (context.Request.Path.StartsWithSegments(_loginAccount, StringComparison.OrdinalIgnoreCase) ||
                !path.Contains("api", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("Skipping RefreshTokenMiddleware for account path without 'api'.");
                await _next(context);
                return;
            }
             var accessToken = context.Request.Cookies["ACCESS_TOKEN"];
            if (IsTokenExpired(accessToken))
            {
                var refreshToken = context.Request.Cookies["REFRESH_TOKEN"];
                if (!string.IsNullOrEmpty(refreshToken))
                {
                    RefreshTokenResponseDto newTokens = await RefreshAccessToken(context, refreshToken);

                    if (newTokens != null)
                    {
                        // Cập nhật cookie với token mới
                        context.Response.Cookies.Append("ACCESS_TOKEN", newTokens.Token, new CookieOptions
                        {
                            HttpOnly = false,
                            Expires = DateTime.UtcNow.AddMinutes(60),
                            IsEssential = true,
                            Secure = true,
                            SameSite = SameSiteMode.None
                        });
                        context.Response.Cookies.Append("REFRESH_TOKEN", newTokens.RefreshToken, new CookieOptions
                        {
                            HttpOnly = false,
                            Expires = DateTime.UtcNow.AddDays(90),
                            IsEssential = true,
                            Secure = true,
                            SameSite = SameSiteMode.None
                        });


                        context.Request.Headers["Authorization"] = $"Bearer {newTokens.Token}";
                        await _next(context);
                        return;
                    }
                }
            }

            await _next(context);
        }

        private bool IsTokenExpired(string token)
        {
            try
            {
                _logger.LogInformation("Checking if token is expired");
                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogInformation("Token is null or empty.");
                    return true;
                }

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken == null)
                {
                    _logger.LogInformation("The token could not be parsed correctly.");
                    return true;
                }

                var expirationTimeUtc = jsonToken.ValidTo;

                if (expirationTimeUtc <= DateTime.UtcNow)
                {
                    _logger.LogInformation($"Token expired at {expirationTimeUtc}. Current UTC time is {DateTime.UtcNow}");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error checking token expiration: {ex.Message}");
                return false;
            }
        }

        private async Task<RefreshTokenResponseDto> RefreshAccessToken(HttpContext context, string refreshToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    if (string.IsNullOrEmpty(refreshToken))
                    {
                        throw new RefreshTokenException("Refresh token cannot be null or empty.");
                    }

                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var (jwtToken, newRefreshToken) = await mediator.Send(new RefreshTokenCommand(refreshToken));

                    // Kiểm tra giá trị null
                    if (string.IsNullOrEmpty(jwtToken) || string.IsNullOrEmpty(newRefreshToken))
                    {
                        throw new RefreshTokenException("Failed to generate new tokens.");
                    }

                    return new RefreshTokenResponseDto
                    {
                        Token = jwtToken,
                        RefreshToken = newRefreshToken
                    };
                }
            }
            catch (RefreshTokenException ex)
            {
                _logger.LogError(ex, "Invalid refresh token: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing token: {Message}", ex.Message);
                return null;
            }
        }
    }
}
