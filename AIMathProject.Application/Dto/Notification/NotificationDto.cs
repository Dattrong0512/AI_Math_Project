using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.Notification
{
    public class NotificationDto
    {
        public int NotificationId { get; set; }

        public int? UserId { get; set; }

        public string? NotificationType { get; set; }

        public string? NotificationTitle { get; set; }

        public string? NotificationMessage { get; set; }

        public DateTime? SentAt { get; set; }

        public string? Status { get; set; }

    }
}
