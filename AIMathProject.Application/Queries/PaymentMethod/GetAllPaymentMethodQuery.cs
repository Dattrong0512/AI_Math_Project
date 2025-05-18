using AIMathProject.Application.Dto.Payment.MethodDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.PaymentMethod
{
    public record GetAllPaymentMethodQuery() : IRequest<ICollection<MethodDto>>;

    public class GetAllPaymentMethodQueryHandler(IPaymentMethodRepository<MethodDto> repository)
        : IRequestHandler<GetAllPaymentMethodQuery, ICollection<MethodDto>>
    {
        public async Task<ICollection<MethodDto>> Handle(GetAllPaymentMethodQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllPaymentMethod();
        }
    }
}
