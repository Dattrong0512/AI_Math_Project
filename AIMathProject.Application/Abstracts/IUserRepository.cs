﻿using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Application.Dto.UserDto;
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
        Task<UserDto> GetInfoUserLogin();
    }
}
