using AIMathProject.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface INotificationRepository<T> where T : class
    {
        public Task<List<T>> GetAllNotificationUser();
        public Task<T> GetNewestNotificationUser();
        public Task<List<T>> GetAllNotificationUserById();
        public Task<bool> PushNotificationForAllUser(NotificationRequestDto requestDto);
        public Task<bool> PushNotificationForUserById(int userId, NotificationRequestDto requestDto);
        public Task<bool> UpdateStatusNotification(int notificationId);
        Task<(List<T> items, int totalCount, int pageIndex, int pageSize)> GetAllNotificationPaginated(int pageIndex, int pageSize);
        Task<(List<T> items, int totalCount, int pageIndex, int pageSize)> GetAllNotificationUserByIdPaginated(int pageIndex, int pageSize);

    }
}
