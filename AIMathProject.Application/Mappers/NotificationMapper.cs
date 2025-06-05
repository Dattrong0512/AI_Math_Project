using AIMathProject.Application.Dto.Notification;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Mappers
{
    public static class NotificationMapper
    {
        public static NotificationDto ToNotificationDto(this Notification notification)
        {
            NotificationDto dto = new NotificationDto
            {
                NotificationId = notification.NotificationId,

                UserId = notification.UserId,

                NotificationType = notification.NotificationType,

                NotificationTitle = notification.NotificationTitle,

                NotificationMessage = notification.NotificationMessage,

                SentAt = notification.SentAt,

                Status = notification.Status
            };
            return dto;
        }
        public static List<NotificationDto> TolistNotificationDto(this ICollection<Notification> list)
        {
            List<NotificationDto> listDto = new List<NotificationDto>();
            foreach(var item in list)
            {
                listDto.Add(item.ToNotificationDto());
            }
            return listDto;
        }
    }
}
