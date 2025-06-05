using AIMathProject.Application.Dto.Notification;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Notification
{
    public record GetNewestNotificationUserQuery() : IRequest<NotificationDto>;
    public class GetNewestNotificationUserQueryHandler(INotificationRepository<NotificationDto> notificationRepository) : IRequestHandler<GetNewestNotificationUserQuery, NotificationDto>
    {
        public async Task<NotificationDto> Handle(GetNewestNotificationUserQuery request, CancellationToken cancellationToken)
        {
            return await notificationRepository.GetNewestNotificationUser();
        }
    }

}
