using AIMathProject.Application.Dto.Notification;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.Notification
{
    public record UpdateStatusNotificationCommand(int notificationId) : IRequest<bool>;
    public class UpdateStatusNotificationCommandHandler : IRequestHandler<UpdateStatusNotificationCommand, bool>
    {
        private readonly INotificationRepository<NotificationDto> _notificationRepository;
        public UpdateStatusNotificationCommandHandler(INotificationRepository<NotificationDto> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<bool> Handle(UpdateStatusNotificationCommand request, CancellationToken cancellationToken)
        {
            return await _notificationRepository.UpdateStatusNotification(request.notificationId);
        }
    }

}
