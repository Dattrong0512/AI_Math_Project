using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Application.Dto.UserDto;
using AIMathProject.Application.Queries.Notification;
using AIMathProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Abstracts
{
    public interface IUserRepository
    {
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
        Task<Pagination<UserDto>> GetInfoUser(int pageIndex, int pageSize);
        Task<Unit> SendEmailConfirm(User user, CancellationToken cancellationToken);
        Task<Pagination<UserDto>> GetUsersWithFilters(string? searchTerm, int? role, bool? status, int pageIndex, int pageSize);
        Task<UserDto> GetInfoUserLogin();

        Task CreateUserWallet(int userId, CancellationToken cancellationToken);

        Task CreateNotification(int UserId);
    }
}
