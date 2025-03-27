using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Abstracts
{
    public interface IAuthTokenProcessor
    {
        Task<(string jwtToken, DateTime expiresAtUtc)> GenerateJwtToken(User user);
        string GenerateRefreshToken();
        void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token, DateTime expiration);
    }
}
