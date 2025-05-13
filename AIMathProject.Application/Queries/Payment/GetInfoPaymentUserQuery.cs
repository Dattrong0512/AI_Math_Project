using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Payment 
{
    public record GetInfoPaymentUserQuery(int idUser) : IRequest<PaymentDto>;
    public class GetInfoPaymentUserQueryHandler(IPaymentRepository<PaymentDto> repository)
        : IRequestHandler<GetInfoPaymentUserQuery, PaymentDto>
    {
        public Task<PaymentDto> Handle(GetInfoPaymentUserQuery request, CancellationToken cancellationToken)
        {
            return repository.GetInfoPaymentUser(request.idUser);
        }
    }
}
