using AIMathProject.Application.Dto.Payment.TokenPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.Payment.CoinTransaction
{
    public class CoinTransactionDto
    {
        public int WalletId { get; set; }
        public int CoinTransactionId { get; set; }

        public int? TokenPackageId { get; set; }

        public bool? IsTokenPackage { get; set; }

        public int? CoinRemains { get; set; }

        public int? Amount { get; set; }

        public DateTime? Date { get; set; }

        public virtual TokenPackageDto? TokenPackage { get; set; }

    }
}
