using AIMathProject.Application.Dto.Notification;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Notification
{
    public record GetAllNotificationByUserId : IRequest<List<NotificationDto>>;
    public class GetAllNotificationByUserIdHandler : IRequestHandler<GetAllNotificationByUserId, List<NotificationDto>>
    {
        private readonly INotificationRepository<NotificationDto> _notificationRepository;
        public GetAllNotificationByUserIdHandler(INotificationRepository<NotificationDto> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<List<NotificationDto>> Handle(GetAllNotificationByUserId request, CancellationToken cancellationToken)
        {
            return await _notificationRepository.GetAllNotificationUserById();
        }
    }

}
