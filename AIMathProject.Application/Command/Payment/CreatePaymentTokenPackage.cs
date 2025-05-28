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
    public record CreatePaymentTokenPackage(int UserID, int TokenPackageId) : IRequest<string>;
    public class CreatePaymentTokenPackageHandler(IWalletRepository<WalletDto> repository) : IRequestHandler<CreatePaymentTokenPackage, string>
    {
        public async Task<string> Handle(CreatePaymentTokenPackage request, CancellationToken cancellationToken)
        {
            return await repository.UpdateCoinBuyToken(request.UserID, request.TokenPackageId);          
        }
    }

}
