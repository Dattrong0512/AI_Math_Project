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
    public record GetAllNotificationQuery() : IRequest<List<NotificationDto>>;
    public class GetAllNotificationQueryHandler(INotificationRepository<NotificationDto> repository) :
        IRequestHandler<GetAllNotificationQuery, List<NotificationDto>>
    {
        public async Task<List<NotificationDto>> Handle(GetAllNotificationQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllNotificationUser();
        }
    }
}
