using AIMathProject.Application.Dto.Payment.CoinTransaction;
using AIMathProject.Application.Dto.Payment.TokenTransaction;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.Payment.Wallet
{
    public class WalletDto
    {
        public int WalletId { get; set; }

        public int UserId { get; set; }

        public int CoinRemains { get; set; }

        public int TokenRemains { get; set; }

        public virtual ICollection<CoinTransactionDto> CoinTransactions { get; set; } = new List<CoinTransactionDto>();

        public virtual ICollection<PaymentDto.PaymentDto> Payments { get; set; } = new List<PaymentDto.PaymentDto>();

        public virtual ICollection<TokenTransactionDto> TokenTransactions { get; set; } = new List<TokenTransactionDto>();

    }
}
