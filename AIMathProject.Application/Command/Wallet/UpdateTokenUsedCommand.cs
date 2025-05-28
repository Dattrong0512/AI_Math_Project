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
    public record UpdateTokenUsedCommand(int walletId, int amount) : IRequest<WalletDto>;
    public class UpdateTokenUsedCommandHandler(IWalletRepository<WalletDto> repository) : IRequestHandler<UpdateTokenUsedCommand, WalletDto>
    {
        public async Task<WalletDto> Handle(UpdateTokenUsedCommand request, CancellationToken cancellationToken)
        {
            return await repository.UpdateTokenUsed(request.walletId, request.amount);
        }
    }

}
