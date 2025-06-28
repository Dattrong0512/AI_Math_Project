using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.PaymentServices.SePay.Model
{
    public class PaymentResponseModel
    {
        public string? UrlPayment { get; set; }
        public string? Method { get; set; }
        public string? OrderDescription { get; set; }
        public decimal? Amount { get; set; }

    }
}
