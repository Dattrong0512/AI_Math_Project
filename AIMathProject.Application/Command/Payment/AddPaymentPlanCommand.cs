using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Application.Dto.Payment.Wallet;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.Payment
{
    public record AddPaymentPlanCommand(PaymentDto dto, int coin) : IRequest<bool>;
    public class AddPaymentPlanCommandHandler(IPaymentRepository<PaymentDto> repository,
        IWalletRepository<WalletDto> walletRepository) : IRequestHandler<AddPaymentPlanCommand, bool>
    {
        public async Task<bool> Handle(AddPaymentPlanCommand request, CancellationToken cancellationToken)
        {   
            await repository.AddPayment(request.dto);
            await walletRepository.UpdateCoinUsed(request.dto.WalletId, -request.coin);
            return true;
        }
    }
}
