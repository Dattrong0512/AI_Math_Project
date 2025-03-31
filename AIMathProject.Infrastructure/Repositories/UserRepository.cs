using AIMathProject.Application.Abstracts;
using AIMathProject.Application.Dto;
using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Application.Dto.UserDto;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Exceptions;
using AIMathProject.Infrastructure.CommonServices;
using AIMathProject.Infrastructure.Data;
using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly LinkGenerator _linkGenerator;

        private readonly UserManager<User> _userManager;

        private readonly IEmailTemplateReader _emailTemplate;

        private readonly IEmailHelper _emailHelper;
        public UserRepository(ApplicationDbContext applicationDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator, UserManager<User> userManager, IEmailTemplateReader emailTemplate, IEmailHelper emailHelper)
        {
            _context = applicationDbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
            _userManager = userManager;
            _emailTemplate = emailTemplate;
            _emailHelper = emailHelper;
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

        public async Task<Unit> SendEmailConfirm(User user, CancellationToken cancellationToken)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string url = _linkGenerator.GetUriByAction(
                    httpContext: _httpContextAccessor.HttpContext,
                    action: "ConfirmEmail",
                    controller: "LoginWithUSPW",
                    values: new { userId = user.Id, token = token },
                    scheme: _httpContextAccessor.HttpContext.Request.Scheme
                );

            string body = await _emailTemplate.GetTemplate("Template\\ConfirmEmail.html");

            body = string.Format(body, user.UserName, url);
            await _emailHelper.SendEmailAsync(new EmailRequest
            {
                To = user.Email,
                Subject = "Confirm Email for Register",
                Content = body
            }, cancellationToken);
            return Unit.Value;
        }
    }
}
