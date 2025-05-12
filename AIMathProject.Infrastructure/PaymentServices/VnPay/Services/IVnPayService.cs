using AIMathProject.Infrastructure.PaymentServices.VnPay.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.PaymentServices.VnPay.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInfomationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);

    }
}
