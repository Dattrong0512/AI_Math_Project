using AIMathProject.Application.Command.Payment;
using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Application.Dto.Payment.PlanDto;
using AIMathProject.Application.Queries.Payment;
using AIMathProject.Application.Queries.Plans;
using AIMathProject.Domain.Entities;
using AIMathProject.Infrastructure.Libraries;
using AIMathProject.Infrastructure.PaymentServices.VnPay.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.PaymentServices.VnPay.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;


        public VnPayService(IConfiguration config, IMediator mediator)
        {
            _config = config;
            _mediator = mediator;
        }

        public async Task<string> CreatePaymentPlansUrl(int idPlan, int idUser, HttpContext context)
        {

            PlansDto dto = await _mediator.Send(new GetInfoPlansQuery(idPlan));

            var vnpay = new VnPayLibrary();
            var vnp_TxnRef = DateTimeOffset.Now.ToUnixTimeSeconds().ToString(); // Sử dụng timestamp giống PHP
            var startTime = DateTime.Now;
            var expireTime = startTime.AddMinutes(15); // Hết hạn sau 15 phút


            string orderInfo = $"Thanh toán gói {dto.PlanName} cho người dùng {idUser}";

            vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"]);
            vnpay.AddRequestData("vnp_Command", _config["VnPay:Command"]);
            vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]); // Đảm bảo khớp với PHP

            vnpay.AddRequestData("vnp_Amount", ((double)dto.Price * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", startTime.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _config["VnPay:CurrCode"]);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", _config["VnPay:Locale"]);
            vnpay.AddRequestData("vnp_OrderInfo", orderInfo); 
            vnpay.AddRequestData("vnp_OrderType", "billpayment"); 
            vnpay.AddRequestData("vnp_ReturnUrl", _config["VnPay:PaymentBackReturnUrl"]);
            vnpay.AddRequestData("vnp_TxnRef", vnp_TxnRef);
            vnpay.AddRequestData("vnp_ExpireDate", expireTime.ToString("yyyyMMddHHmmss")); 

            var paymentUrl = vnpay.CreateRequestUrl(_config["VnPay:BaseUrl"], _config["VnPay:HashSecret"]);
            Debug.WriteLine("Payment URL: " + paymentUrl);
            return paymentUrl;
        }

        public async Task<PaymentResponseModel> PaymentExecute(IQueryCollection collections)
        {
            var vnpay = new VnPayLibrary();
            foreach (var (key, value) in collections)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            var vnp_TxnRef = vnpay.GetResponseData("vnp_TxnRef");
            var vnp_OrderId = vnp_TxnRef; // Sử dụng trực tiếp vnp_TxnRef
            var vnp_TransactionID = vnpay.GetResponseData("vnp_TransactionNo");
            var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
            var vnp_Amount = vnpay.GetResponseData("vnp_Amount");
            // Phân tích vnp_OrderInfo để lấy PlanName và idUser
            int userId = 0;
            string planName = string.Empty;
            if (!string.IsNullOrEmpty(vnp_OrderInfo))
            {
                try
                {
                    // vnp_OrderInfo có dạng: "Thanh toán gói {dto.PlanName} cho người dùng {idUser}"
                    var parts = vnp_OrderInfo.Split(' ');
                    if (parts.Length >= 5)
                    {
                        // Lấy idUser từ phần tử cuối
                        int.TryParse(parts[parts.Length - 1], out userId);
                        // Lấy PlanName từ sau "gói" đến trước "cho người dùng"
                        planName = string.Join(" ", parts.Skip(2).Take(parts.Length - 4));
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error parsing vnp_OrderInfo: {ex.Message}");
                }
            }
            int planId = 0;
            if (planName == "Gói 1 đề")
            {
                planId = 1;
            }
            else if (planName == "Gói 5 đề")
            {
                planId = 2;
            }
            else
            {
                planId = 3;
            }

                var checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["VnPay:HashSecret"]);
            if (!checkSignature)
            {
                return new PaymentResponseModel { Success = false };
            }


            DateTime vietNamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            PaymentDto paymentDto = new PaymentDto
            {
                MethodId = 1,
                UserId = userId,
                TokenPackageId = null,
                PlanId = planId,
                Date = vietNamTime,
                Description = vnp_OrderInfo,
                Status = "Success",
                Price = decimal.Parse(vnp_Amount),
                OrderID = vnp_OrderId,
                TransactionID = vnp_TransactionID
            };

            bool paymentStatus = await _mediator.Send(new AddPaymentPlanCommand(paymentDto));

            return new PaymentResponseModel
            {
                Success = paymentStatus,
                PaymentMethod = "VnPay",
                OrderDescription = vnp_OrderInfo,
                OrderId = vnp_OrderId,
                TransactionId = vnp_TransactionID,
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode
            };
        }


    }
}