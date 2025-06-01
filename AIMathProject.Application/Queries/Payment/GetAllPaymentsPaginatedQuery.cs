using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Payment
{
    public record GetAllPaymentsPaginatedQuery(int PageIndex, int PageSize) : IRequest<Pagination<PaymentDto>>;

    public class GetAllPaymentsPaginatedQueryHandler : IRequestHandler<GetAllPaymentsPaginatedQuery, Pagination<PaymentDto>>
    {
        private readonly IPaymentRepository<PaymentDto> _paymentRepository;

        public GetAllPaymentsPaginatedQueryHandler(IPaymentRepository<PaymentDto> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Pagination<PaymentDto>> Handle(GetAllPaymentsPaginatedQuery request, CancellationToken cancellationToken)
        {
            var (items, totalCount, pageIndex, pageSize) = await _paymentRepository.GetAllPaymentsPaginated(request.PageIndex, request.PageSize);

            return new Pagination<PaymentDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }
    }
}