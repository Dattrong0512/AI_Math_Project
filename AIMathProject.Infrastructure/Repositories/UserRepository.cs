using AIMathProject.Application.Abstracts;
using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Application.Dto.UserDto;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Exceptions;
using AIMathProject.Infrastructure.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(ApplicationDbContext applicationDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = applicationDbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Pagination<UserDto>> GetInfoUser(int pageIndex, int pageSize)
        {
            List<User> listUser = await _context.Users
                                        .OrderBy(u => u.Id)
                                        .Skip(pageIndex * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

            Pagination<UserDto> pagination = new Pagination<UserDto>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = await _context.Users.CountAsync(),
                Items = _mapper.Map<List<UserDto>>(listUser) // Sửa ở đây
            };
            return pagination;
        }

        public async Task<UserDto> GetInfoUserLogin()
        {
            // Lấy thông tin claims từ access token
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new UnauthorizedAccessException("Cannot found infomation user in token");
            }

            var userId = int.Parse(userIdClaim); // Hoặc int.Parse nếu Id là int

            // Truy vấn thông tin user từ database
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NoDataFoundException("user");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }
    }
}
