using AIMathProject.Application.Abstracts;
using AIMathProject.Domain.Entities;
using AIMathProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }
    }
}
