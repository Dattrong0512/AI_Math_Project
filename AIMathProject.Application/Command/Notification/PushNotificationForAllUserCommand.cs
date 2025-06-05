using AIMathProject.Application.Dto.Notification;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Domain.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.Notification
{
    public record PushNotificationForAllUserCommand(NotificationRequestDto notificationRequestDto) : IRequest<bool>;
    public class PushNotificationForAllUserCommandHandler : IRequestHandler<PushNotificationForAllUserCommand, bool>
    {
        private readonly INotificationRepository<NotificationDto> _notificationRepository;
        public PushNotificationForAllUserCommandHandler(INotificationRepository<NotificationDto> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<bool> Handle(PushNotificationForAllUserCommand request, CancellationToken cancellationToken)
        {
            return await _notificationRepository.PushNotificationForAllUser(request.notificationRequestDto);
        }
    }

}
