using AIMathProject.Application.Dto.Notification;
using AIMathProject.Application.Mappers;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Domain.Requests;
using AIMathProject.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class NotificationReposioty : INotificationRepository<NotificationDto>
    {
        public readonly ILogger<NotificationReposioty> _logger;
        public readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public NotificationReposioty(ILogger<NotificationReposioty> logger, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<NotificationDto>> GetAllNotificationUser()
        {
            List<Notification> list = await _context.Notifications.ToListAsync();

            return list.TolistNotificationDto();

        }

        public async Task<List<NotificationDto>> GetAllNotificationUserById()
        {
            string userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int idUs = int.Parse(userIdClaim);
            List<Notification> list = await _context.Notifications.Where(us => us.UserId == idUs).ToListAsync();
            return list.TolistNotificationDto();                                    
        }

        public async Task<NotificationDto> GetNewestNotificationUser()
        {
            string userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int idUs = int.Parse(userIdClaim);
            Notification notification = await _context.Notifications
                                        .Where(n => n.UserId == idUs)
                                        .OrderByDescending(n => n.SentAt) // Sửa từ OrderDescending thành OrderByDescending
                                        .FirstOrDefaultAsync(); // Sửa từ FirstOrdefaulstAysnc thành FirstOrDefaultAsync
            return notification.ToNotificationDto();
                        
        }

        public async Task<bool> PushNotificationForAllUser(NotificationRequestDto requestDto)
        {
            List<int> list = new List<int>();
            list = _context.Users
                .Select(u => u.Id)
                .Distinct()
                .ToList();
            foreach (var userId in list)
            {
                Notification notification = new Notification
                {
                    UserId = userId,
                    NotificationType = requestDto.NotificationType,
                    NotificationTitle = requestDto.NotificationTitle,
                    NotificationMessage = requestDto.NotificationMessage,
                    SentAt = DateTime.Now,
                    Status = "Unread",
                };
                _context.Notifications.Add(notification);
            }
            int row_effect = await _context.SaveChangesAsync();
            if (row_effect > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> PushNotificationForUserById(int userId, NotificationRequestDto requestDto)
        {
            Notification notification = new Notification
            {
                UserId = userId,
                NotificationType = requestDto.NotificationType,
                NotificationTitle = requestDto.NotificationTitle,
                NotificationMessage = requestDto.NotificationMessage,
                SentAt = DateTime.Now,
                Status = "Unread",
            };
            _context.Notifications.Add(notification);
            int row_effect = await _context.SaveChangesAsync();
            if (row_effect > 0)
            {
                return true;
            }
            return false;
        }

        public Task<bool> UpdateStatusNotification(int notificationId)
        {
            string userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int idUs = int.Parse(userIdClaim);
            Notification? notification = _context.Notifications.FirstOrDefault(n => n.NotificationId == notificationId && n.UserId == idUs);
            if (notification == null)
            {
                _logger.LogWarning($"Notification with ID {notificationId} not found for user ID {idUs}");
                return Task.FromResult(false);
            }
            notification.Status = "Read";
            _context.Notifications.Update(notification);
            int row_effect = _context.SaveChanges();
            if (row_effect > 0)
            {
                return Task.FromResult(true);
            }
            _logger.LogError($"Failed to update notification status for ID {notificationId} and user ID {idUs}");
            return Task.FromResult(false);
        }

        public async Task<(List<NotificationDto> items, int totalCount, int pageIndex, int pageSize)> GetAllNotificationPaginated(int pageIndex, int pageSize)
        {
            try
            {
                var totalCount = await _context.Notifications.CountAsync();

                var notifications = await _context.Notifications
                    .OrderByDescending(n => n.SentAt)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var notificationDtos = notifications.TolistNotificationDto();

                return (notificationDtos, totalCount, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paginated notifications");
                throw;
            }
        }

        public async Task<(List<NotificationDto> items, int totalCount, int pageIndex, int pageSize)> GetAllNotificationUserByIdPaginated(int pageIndex, int pageSize)
        {
            try
            {
                string userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int idUs = int.Parse(userIdClaim);

                var totalCount = await _context.Notifications
                    .Where(n => n.UserId == idUs)
                    .CountAsync();

                var notifications = await _context.Notifications
                    .Where(n => n.UserId == idUs)
                    .OrderByDescending(n => n.SentAt)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var notificationDtos = notifications.TolistNotificationDto();

                return (notificationDtos, totalCount, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paginated notifications for user");
                throw;
            }
        }
    }
}
