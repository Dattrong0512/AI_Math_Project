using AIMathProject.Application.Abstracts;
using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Application.Dto.UserDto;
using AIMathProject.Domain.Entities;
using AIMathProject.Infrastructure.Data;
using AutoMapper;
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
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _context = applicationDbContext;
            _mapper = mapper;
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

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }
    }
}
