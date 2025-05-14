using AIMathProject.Application.Command.Payment;
using AIMathProject.Application.Command.PlansUser;
using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Application.Dto.Payment.PlanDto;
using AIMathProject.Application.Dto.Payment.TokenPackageDto;
using AIMathProject.Application.Queries.Plans;
using AIMathProject.Application.Queries.TokenPackage;
using AIMathProject.Domain.Entities;
using AIMathProject.Infrastructure.Libraries;
using AIMathProject.Infrastructure.PaymentServices.VnPay.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AIMathProject.Infrastructure.PaymentServices.VnPay.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;
        private readonly ILogger<VnPayService> _logger;

        public VnPayService(IConfiguration config, IMediator mediator, ILogger<VnPayService> logger)
        {
            _config = config;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<string> CreatePayment(bool isPlan, int id, int idUser, HttpContext context)
        {

            string orderInfo = "";
            double price = 0;
            if (isPlan)
            {
                PlansDto dto = await _mediator.Send(new GetInfoPlansQuery(id));
                orderInfo = $"Thanh toán {dto.PlanName} cho người dùng {idUser} ";
                price = (double)dto.Price * 100;
            }
            else
            {
                TokenPackageDto dto = await _mediator.Send(new GetInfoTokenPackageByIdQuery(id));
                orderInfo = $"Thanh toán {dto.PackageName} cho người dùng {idUser} ";
                price = (double)dto.Price * 100;
            }
            
            var vnpay = new VnPayLibrary();
            var vnp_TxnRef = DateTimeOffset.Now.ToUnixTimeSeconds().ToString(); // Sử dụng timestamp giống PHP
            var startTime = DateTime.Now;
            var expireTime = startTime.AddMinutes(15); // Hết hạn sau 15 phút


            vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"]);
            vnpay.AddRequestData("vnp_Command", _config["VnPay:Command"]);
            vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]); // Đảm bảo khớp với PHP

            vnpay.AddRequestData("vnp_Amount", price.ToString());
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

            string errorMessage = string.Empty;
            bool isSuccess = false;
            string status = "Failed";

            switch (vnp_ResponseCode)
            {
                case "00":
                    isSuccess = true;
                    status = "Success";
                    break;
                case "07":
                    errorMessage = "Trừ tiền thành công nhưng giao dịch bị nghi ngờ (liên quan tới lừa đảo, giao dịch bất thường).";
                    break;
                case "09":
                    errorMessage = "Thẻ/Tài khoản chưa đăng ký dịch vụ Internet Banking.";
                    break;
                case "10":
                    errorMessage = "Xác thực thông tin thẻ/tài khoản không đúng quá 3 lần.";
                    break;
                case "11":
                    errorMessage = "Đã hết hạn chờ thanh toán. Vui lòng thực hiện lại giao dịch.";
                    break;
                case "12":
                    errorMessage = "Thẻ/Tài khoản bị khóa.";
                    break;
                case "13":
                    errorMessage = "Nhập sai mật khẩu xác thực giao dịch (OTP). Vui lòng thực hiện lại.";
                    break;
                case "24":
                    errorMessage = "Khách hàng hủy giao dịch.";
                    break;
                case "51":
                    errorMessage = "Tài khoản không đủ số dư để thực hiện giao dịch.";
                    break;
                case "65":
                    errorMessage = "Tài khoản đã vượt quá hạn mức giao dịch trong ngày.";
                    break;
                case "75":
                    errorMessage = "Ngân hàng thanh toán đang bảo trì.";
                    break;
                case "79":
                    errorMessage = "Nhập sai mật khẩu thanh toán quá số lần quy định. Vui lòng thực hiện lại.";
                    break;
                default:
                    errorMessage = "Lỗi không xác định. Vui lòng liên hệ hỗ trợ.";
                    break;
            }
            // Phân tích vnp_OrderInfo để lấy PlanName và idUser
            int userId = 0;
            string planOrPackage = string.Empty;
 
            if (!string.IsNullOrEmpty(vnp_OrderInfo))
            {
                try
                {
                    // vnp_OrderInfo có dạng: "Thanh toán gói {dto.PlanName} cho người dùng {idUser}"
                    var parts = vnp_OrderInfo.Split(new[] { "Thanh toán ", " cho người dùng " }, StringSplitOptions.RemoveEmptyEntries);
                    planOrPackage = parts[0];
                    // Lấy idUser từ phần tử cuối
                    int.TryParse(parts[1], out userId);
                    
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error parsing vnp_OrderInfo: {ex.Message}");
                }
            }

            bool isPlan = false;
            int planId = 0;
            int packageId = 0;
             planId = planOrPackage switch
            {
                "Gói 1 đề" => 1,
                "Gói 5 đề" => 2,
                "Gói 10 đề" => 3,
                _ => 0
            };
          
            packageId = planOrPackage switch
            {
                "Gói Normal" => 1,
                "Gói Vip" => 2,
                "Gói SVip" => 3,
                _ => 0
            };
            isPlan = planId > 0 ? true : false;

            var checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["VnPay:HashSecret"]);
            if (!checkSignature)
            {
                return new PaymentResponseModel { Success = false };
            }

            bool paymentStatus = false;
            bool planUser = false;
            bool packageUser = false;

            if (isSuccess)
            {
                DateTime vietNamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
                PaymentDto paymentDto = new PaymentDto
                {
                    MethodId = 1,
                    UserId = userId,
                    TokenPackageId = packageId > 0 ? packageId : null,
                    PlanId = planId > 0 ? planId : null,
                    Date = vietNamTime,
                    Description = vnp_OrderInfo,
                    Status = status,
                    Price = decimal.Parse(vnp_Amount) / 100,
                    OrderID = vnp_OrderId,
                    TransactionID = vnp_TransactionID
                };

                paymentStatus = await _mediator.Send(new AddPaymentPlanCommand(paymentDto));
                if (isPlan)
                {
                    PlanUser plUser = new PlanUser
                    {

                        UserId = userId,

                        Coins = planId == 1 ? 1 : planId == 2 ? 5 : 10
                    };
                    planUser = await _mediator.Send(new AddPlanUserCommand(plUser));
                }

            }
            _logger.LogInformation(vnp_ResponseCode);
            return new PaymentResponseModel
            {
                Success = paymentStatus,
                PaymentMethod = "VnPay",
                OrderDescription = vnp_OrderInfo,
                OrderId = vnp_OrderId,
                TransactionId = vnp_TransactionID,
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode,
                ErrorMessage = errorMessage
            };
            
        }


    }
}