using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Payment
{
    public record GetLatestInfoPayment(int userId) : IRequest<PaymentDto>;
    public class GetLatestInfoPaymentHandler(IPaymentRepository<PaymentDto> repository) : IRequestHandler<GetLatestInfoPayment, PaymentDto>
    {
        public Task<PaymentDto> Handle(GetLatestInfoPayment request, CancellationToken cancellationToken)
        {
            return repository.GetLatestPayment(request.userId);
        }
    }
}
