using AIMathProject.Application.Abstracts;
using AIMathProject.Application.Dto;
using AIMathProject.Application.Dto.Notification;
using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Application.Dto.UserDto;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Exceptions;
using AIMathProject.Domain.Requests;
using AIMathProject.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AIMathProject.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly LinkGenerator _linkGenerator;

        private readonly UserManager<User> _userManager;

        private readonly ITemplateReader _emailTemplate;

        private readonly IEmailHelper _emailHelper;
        public UserRepository(ApplicationDbContext applicationDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator, UserManager<User> userManager, ITemplateReader emailTemplate, IEmailHelper emailHelper)
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

            string body = await _emailTemplate.GetTemplate("Template/ConfirmEmail.html");

            body = string.Format(body, user.UserName, url);
            await _emailHelper.SendEmailAsync(new EmailRequest
            {
                To = user.Email,
                Subject = "Confirm Email for Register",
                Content = body
            }, cancellationToken);
            return Unit.Value;
        }

        public async Task<Pagination<UserDto>> GetUsersWithFilters(string? searchTerm, int? role, bool? status, int pageIndex, int pageSize)
        {
            // Start with the base query
            IQueryable<User> query = _context.Users;
            
            // Search
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(u =>
                    u.Email.ToLower().Contains(searchTerm) ||
                    u.UserName.ToLower().Contains(searchTerm));
            }

            // Role
            if (role.HasValue)
            {
                if (role.Value == 1) // User role
                {
                    query = from user in query
                            join userRole in _context.UserRoles on user.Id equals userRole.UserId
                            join r in _context.Roles on userRole.RoleId equals r.Id
                            where r.Name == "User"
                            select user;
                }
                else if (role.Value == 2) // Admin role
                {
                    query = from user in query
                            join userRole in _context.UserRoles on user.Id equals userRole.UserId
                            join r in _context.Roles on userRole.RoleId equals r.Id
                            where r.Name == "Admin"
                            select user;
                }
            }

            // Status
            if (status.HasValue)
            {
                query = query.Where(u => u.Status == status.Value);
            }

            var totalCount = await query.CountAsync();

            var users = await query
                .OrderBy(u => u.UserName)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            Pagination<UserDto> pagination = new Pagination<UserDto>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = _mapper.Map<List<UserDto>>(users)
            };

            return pagination;
        }

        public async Task CreateUserWallet(int userId, CancellationToken cancellationToken)
        {
            var wallet = new Wallet
            {
                UserId = userId,
                CoinRemains = 0,
                TokenRemains = 0
            };

            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CreateNotification(int UserId)
        {
            if(UserId != null)
            {
                Notification noti = new Notification()
                {
                    UserId = UserId,
                    NotificationType = "Info",
                    NotificationTitle = "Chào mừng",
                    NotificationMessage = "Chào mừng thành viên mới",
                    SentAt = DateTime.Now,
                    Status = "Unread"                  
                }
                ;
                _context.Notifications.Add(noti);
                await _context.SaveChangesAsync();
            }
        }
    }
}
