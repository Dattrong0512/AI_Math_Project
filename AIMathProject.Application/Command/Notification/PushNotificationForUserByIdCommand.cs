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
    public record PushNotificationForUserByIdCommand(int userId, NotificationRequestDto requestDto) : IRequest<bool>;
    public class PushNotificationForUserByIdCommandHandler : IRequestHandler<PushNotificationForUserByIdCommand, bool>
    {
        private readonly INotificationRepository<NotificationDto> _notificationRepository;
        public PushNotificationForUserByIdCommandHandler(INotificationRepository<NotificationDto> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<bool> Handle(PushNotificationForUserByIdCommand request, CancellationToken cancellationToken)
        {
            return await _notificationRepository.PushNotificationForUserById(request.userId, request.requestDto);
        }
    }

}
