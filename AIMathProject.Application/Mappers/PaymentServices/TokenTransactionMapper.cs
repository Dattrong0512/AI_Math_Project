using AIMathProject.Application.Dto.Payment.TokenTransaction;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Mappers.PaymentServices
{
    public static class TokenTransactionMapper
    {

        public static TokenTransactionDto ToTokenTransactionDto(this TokenTransaction transaction)
        {
            TokenTransactionDto dto = new TokenTransactionDto
            {
                TokenTransactionId = transaction.TokenTransactionId,
                WalletId = transaction.WalletId,
                TokenRemains = transaction.TokenRemains,
                TokenAmount = transaction.TokenAmount,
                Date = transaction.Date,
             
            };
            return dto;
        }
        public static List<TokenTransactionDto> ToListTokenTransactionDto(ICollection<TokenTransaction> list)
        {
            List<TokenTransactionDto> dto = new List<TokenTransactionDto>();
            foreach (var item in list)
            {
                dto.Add(item.ToTokenTransactionDto());
            }
            return dto;
        }
    }
}
