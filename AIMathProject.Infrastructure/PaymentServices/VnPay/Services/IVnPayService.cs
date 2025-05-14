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
        Task<string> CreatePayment(bool isPlan, int idPlan, int IdUser, HttpContext context);
        Task<PaymentResponseModel> PaymentExecute(IQueryCollection collections);

    }
}
