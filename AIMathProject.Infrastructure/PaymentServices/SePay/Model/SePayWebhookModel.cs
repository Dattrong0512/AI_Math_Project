using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.PaymentServices.SePay.Model
{
    public class SePayWebhookModel
    {
        public int? id { get; set; }
        public string? gateway { get; set; }
        public string? transactionDate { get; set; }
        public string? accountNumber { get; set; }
        public string? code { get; set; }
        public string? content { get; set; }
        public string? transferType { get; set; }
        public decimal? transferAmount { get; set; }
        public decimal? accumulated { get; set; }
        public string? subAccount { get; set; }
        public string? referenceCode { get; set; }
        public string? description { get; set; }
    }
}
