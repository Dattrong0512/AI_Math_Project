using AIMathProject.Application.Dto.Payment.CoinTransaction;
using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Application.Dto.Payment.TokenTransaction;
using AIMathProject.Application.Dto.Payment.Wallet;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Mappers.PaymentServices
{
    public static class WalletMapper
    {
        public static WalletDto ToWalletDto(this Wallet wallet)
        {
            WalletDto dto = new WalletDto
            {
                WalletId = wallet.WalletId,
                UserId = wallet.UserId,
                CoinRemains = wallet.CoinRemains,
                TokenRemains = wallet.TokenRemains,
                CoinTransactions = CoinTransactionMapper.ToListCoinTransactionDto(wallet.CoinTransactions),
                Payments = PaymentMapper.ToListPaymentDto(wallet.Payments),
                TokenTransactions = TokenTransactionMapper.ToListTokenTransactionDto(wallet.TokenTransactions)
            };
            return dto;
        }
    }
}
