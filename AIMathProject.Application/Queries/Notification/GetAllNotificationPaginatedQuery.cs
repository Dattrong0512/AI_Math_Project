using AIMathProject.Application.Dto.Notification;
using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Notification
{
    public record GetAllNotificationPaginatedQuery(int PageIndex, int PageSize) : IRequest<Pagination<NotificationDto>>;

    public class GetAllNotificationPaginatedQueryHandler : IRequestHandler<GetAllNotificationPaginatedQuery, Pagination<NotificationDto>>
    {
        private readonly INotificationRepository<NotificationDto> _notificationRepository;

        public GetAllNotificationPaginatedQueryHandler(INotificationRepository<NotificationDto> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Pagination<NotificationDto>> Handle(GetAllNotificationPaginatedQuery request, CancellationToken cancellationToken)
        {
            var (items, totalCount, pageIndex, pageSize) = await _notificationRepository.GetAllNotificationPaginated(request.PageIndex, request.PageSize);

            return new Pagination<NotificationDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }
    }
}