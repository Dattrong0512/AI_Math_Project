using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.Payment.TokenTransaction
{
    public class TokenTransactionDto
    {
        public int TokenTransactionId { get; set; }

        public int WalletId { get; set; }

        public int? TokenRemains { get; set; }

        public int? TokenAmount { get; set; }

        public DateTime? Date { get; set; }

    }
}
