using AIMathProject.Application.Dto.Notification;
using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Notification
{
    public record GetAllNotificationByUserIdPaginatedQuery(int PageIndex, int PageSize) : IRequest<Pagination<NotificationDto>>;

    public class GetAllNotificationByUserIdPaginatedQueryHandler : IRequestHandler<GetAllNotificationByUserIdPaginatedQuery, Pagination<NotificationDto>>
    {
        private readonly INotificationRepository<NotificationDto> _notificationRepository;

        public GetAllNotificationByUserIdPaginatedQueryHandler(INotificationRepository<NotificationDto> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Pagination<NotificationDto>> Handle(GetAllNotificationByUserIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            var (items, totalCount, pageIndex, pageSize) = await _notificationRepository.GetAllNotificationUserByIdPaginated(request.PageIndex, request.PageSize);

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