using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.Payment
{
    public record AddPaymentPlanCommand(PaymentDto dto) : IRequest<bool>;
    public class AddPaymentPlanCommandHandler(IPaymentRepository<PaymentDto> repository) : IRequestHandler<AddPaymentPlanCommand, bool>
    {
        public async Task<bool> Handle(AddPaymentPlanCommand request, CancellationToken cancellationToken)
        {
            return await repository.AddPayment(request.dto);
        }
    }
}
