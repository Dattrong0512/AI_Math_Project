using AIMathProject.Application.Dto.Payment.Wallet;
using AIMathProject.Domain.Interfaces;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.Wallet
{
    public record UpdateCoinUsedCommand(int walletId, int amount) : IRequest<WalletDto>;
    public class UpdateCoinUsedCommandHandler(IWalletRepository<WalletDto> repository) : IRequestHandler<UpdateCoinUsedCommand, WalletDto>
    {
        public async Task<WalletDto> Handle(UpdateCoinUsedCommand request, CancellationToken cancellationToken)
        {
            return await repository.UpdateCoinUsed(request.walletId, request.amount);
        }
    }

}
