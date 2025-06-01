using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Payment
{
    public record GetAllPaymentsFilterByDateQuery(DateTime? StartDate = null, DateTime? EndDate = null) : IRequest<List<PaymentDto>>;

    public class GetAllPaymentsFilterByDateQueryHandler : IRequestHandler<GetAllPaymentsFilterByDateQuery, List<PaymentDto>>
    {
        private readonly IPaymentRepository<PaymentDto> _paymentRepository;

        public GetAllPaymentsFilterByDateQueryHandler(IPaymentRepository<PaymentDto> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<List<PaymentDto>> Handle(GetAllPaymentsFilterByDateQuery request, CancellationToken cancellationToken)
        {
            return await _paymentRepository.GetAllPaymentsFilterByDate(request.StartDate, request.EndDate);
        }
    }
}