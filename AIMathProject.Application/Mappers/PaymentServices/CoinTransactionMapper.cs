using AIMathProject.Application.Dto.Payment.CoinTransaction;
using AIMathProject.Application.Dto.Payment.TokenPackage;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Mappers.PaymentServices
{
    public static class CoinTransactionMapper
    {
        public static CoinTransactionDto ToCoinTransactionDto(this CoinTransaction coinTransaction)
        {
            CoinTransactionDto dto = new CoinTransactionDto
            {
                WalletId = coinTransaction.WalletId,
                CoinTransactionId = coinTransaction.CoinTransactionId,

                TokenPackageId = coinTransaction.TokenPackageId,

                IsTokenPackage = coinTransaction.IsTokenPackage,

                CoinRemains = coinTransaction.CoinRemains,

                Amount = coinTransaction.Amount,

                Date = coinTransaction.Date,

                TokenPackage = coinTransaction.TokenPackage.ToTokenPackageDto()
            };
            return dto;
        }
        public static List<CoinTransactionDto> ToListCoinTransactionDto(ICollection<CoinTransaction> list)
        {
            List<CoinTransactionDto> dto = new List<CoinTransactionDto>();
            foreach (var item in list)
            {
                dto.Add(item.ToCoinTransactionDto());
            }
            return dto;
        }
        public static CoinTransaction ToCoinTransaction(this CoinTransactionDto dto)
        {
            CoinTransaction coinTransaction = new CoinTransaction
            {
                WalletId = dto.WalletId,
                IsTokenPackage = dto.IsTokenPackage,
                CoinRemains = dto.CoinRemains,
                Amount = dto.Amount,
                Date = dto.Date,
                TokenPackage = dto.TokenPackage != null ? dto.TokenPackage.ToTokenPackage() : null,
            };
            return coinTransaction;
        }
    }
}
