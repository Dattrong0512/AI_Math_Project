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
    public record GetAllPaymentInfoQuery(int userId) : IRequest<List<PaymentDto>>;
    public class GetAllPaymentInfoQueryHandler(IPaymentRepository<PaymentDto> repository) : IRequestHandler<GetAllPaymentInfoQuery, List<PaymentDto>>
    {
        public Task<List<PaymentDto>> Handle(GetAllPaymentInfoQuery request, CancellationToken cancellationToken)
        {
           return repository.GetAllPayment(request.userId);
        }
    }


}
