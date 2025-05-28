using AIMathProject.Application.Dto.Payment.Wallet;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Wallet
{
    public record GetWalletByUserIdQuery(int UserId) : IRequest<WalletDto>;
    public class GetWalletByUserIdQueryHandler : IRequestHandler<GetWalletByUserIdQuery, WalletDto>
    {
        // Assuming you have a repository or service to get the wallet by user ID
        private readonly IWalletRepository<WalletDto> _walletRepository;

        public GetWalletByUserIdQueryHandler(IWalletRepository<WalletDto> walletRepository)
        {
            _walletRepository = walletRepository;
        }
        public async Task<WalletDto> Handle(GetWalletByUserIdQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetWalletByUserId(request.UserId);
            return wallet;
        }
    }

}
