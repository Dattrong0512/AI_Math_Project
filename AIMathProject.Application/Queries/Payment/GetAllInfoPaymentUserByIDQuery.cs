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
    public record GetAllInfoPaymentUserByIDQuery(int id) : IRequest<ICollection<PaymentDto>>;

    public class GetAllInfoPaymentUserByIDQueryHandler(IPaymentRepository<PaymentDto> repository) :
        IRequestHandler<GetAllInfoPaymentUserByIDQuery, ICollection<PaymentDto>>
    {
        public async Task<ICollection<PaymentDto>> Handle(GetAllInfoPaymentUserByIDQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllInfoPaymentUserById(request.id);
        }
    }
}
