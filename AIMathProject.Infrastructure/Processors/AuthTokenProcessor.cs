using AIMathProject.Application.Abstracts;
using AIMathProject.Domain.Entities;
using AIMathProject.Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Processors
{
    public class AuthTokenProcessor : IAuthTokenProcessor
    {
        private readonly JwtOptions _jwtOptions;

        private readonly IHttpContextAccessor _contextAccessor;

        private readonly UserManager<User> _userManager;
        public AuthTokenProcessor(IOptions<JwtOptions> jwtOptions, IHttpContextAccessor ContextAccessor, UserManager<User> userManager)
        {
            _jwtOptions = jwtOptions.Value;
            _contextAccessor = ContextAccessor;
            _userManager = userManager;
        }
        public async Task<(string jwtToken, DateTime expiresAtUtc)> GenerateJwtToken(User user)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var creadentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            bool checkRoleAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", checkRoleAdmin == true ? "Admin" : "User"),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };
            var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationTimeInMinutes);
            var Token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creadentials
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(Token);
            return (jwtToken, expires);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token, DateTime expiration)
        {
            _contextAccessor.HttpContext.Response.Cookies.Append(cookieName, token, new CookieOptions { 
                HttpOnly = true,
                Expires = expiration,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

        }

    }
}
