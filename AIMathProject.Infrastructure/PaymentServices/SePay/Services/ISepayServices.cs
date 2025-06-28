using AIMathProject.Infrastructure.PaymentServices.SePay.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.PaymentServices.SePay.Services
{
    public interface ISepayServices
    {
        public Task<PaymentResponseModel> CreateUrlPay(int idPlan, int IdUser);

        public Task<string> PaymentCallBack(SePayWebhookModel sePayWebhookModel);
    }
}
