using AIMathProject.Application.Command.Payment;
using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Application.Dto.Payment.PlanDto;
using AIMathProject.Application.Queries.Plans;
using AIMathProject.Domain.Entities;
using AIMathProject.Infrastructure.Data;
using AIMathProject.Infrastructure.PaymentServices.SePay.Model;
using AIMathProject.Infrastructure.PaymentServices.VnPay.Services;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.PaymentServices.SePay.Services
{
    public class SePayServices : ISepayServices
    {
        private readonly IConfiguration _config;
        private readonly ILogger<SePayServices> _logger;
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SePayServices(IConfiguration config, IMediator mediator, ApplicationDbContext context, ILogger<SePayServices> logger, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _mediator = mediator;
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<PaymentResponseModel> CreateUrlPay(int id, int idUser)
        {


            string orderInfo;
            int amount = 0;

            PlansDto dto = await _mediator.Send(new GetInfoPlansQuery(id));
            orderInfo = $"Thanh toán {dto.PlanName} cho người dùng {idUser} ";
            amount = (int)dto.Price;

            string acc = _config["SePay:Account"];
            string bank = _config["SePay:Bank"];
            string paymentUrl = $"https://qr.sepay.vn/img?acc={acc}&bank={bank}";
            if (amount > 0)
                paymentUrl += $"&amount={amount}";

            if (!string.IsNullOrWhiteSpace(orderInfo))
                paymentUrl += $"&des={Uri.EscapeDataString(orderInfo)}";

            return new PaymentResponseModel
            {
                UrlPayment = paymentUrl,
                Method = "SePay",
                OrderDescription = orderInfo,
                Amount = amount
            };

        }

        public async Task<string> PaymentCallBack(SePayWebhookModel sePayWebhookModel)
        {

            int userId = 0;
            string plan = string.Empty;

            if (!string.IsNullOrEmpty(sePayWebhookModel.content))
            {
                try
                {
                    // Ví dụ chuỗi: "NHAN TU ... Thanh toan Goi 1 coin cho nguoi dung 1"
                    string description = sePayWebhookModel.content;

                    // Tìm đoạn chứa thông tin bắt đầu từ "Thanh toan"
                    int startIndex = description.IndexOf("Thanh toan", StringComparison.OrdinalIgnoreCase);
                    if (startIndex >= 0)
                    {
                        string relevantPart = description.Substring(startIndex); // Lấy từ "Thanh toan ..." trở đi

                        // Tách theo " cho nguoi dung "
                        var parts = relevantPart.Split(new[] { " cho nguoi dung " }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 2)
                        {
                            // Lấy phần kế trước là tên gói (loại bỏ "Thanh toan " nếu có)
                            plan = parts[0].Replace("Thanh toan ", "", StringComparison.OrdinalIgnoreCase).Trim();

                            // Lấy user ID
                            int.TryParse(parts[1].Trim(), out userId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error parsing description: {ex.Message}");
                }
            }
            int planId = 0;
            int coin = 0;
            planId = plan switch
            {
                "Goi 1 coin" => 1,
                "Goi 5 coin" => 2,
                "Goi 10 coin" => 3,
                _ => 0
            };
            coin = planId
                switch
            {
                1 => 1,
                2 => 5,
                3 => 10,
                _ => 0
            };

            int wallet_id = _context.Wallets.Where(wl => wl.UserId == userId)
                    .Select(wl => wl.WalletId)
                    .FirstOrDefault();
            if (wallet_id == 0)
            {
                Wallet wallet = new Wallet
                {
                    UserId = userId,
                    CoinRemains = 0,
                    TokenRemains = 0
                };
                _context.Wallets.Add(wallet);
                _context.SaveChanges();
                wallet_id = wallet.WalletId;
            }
            DateTime vietNamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            PaymentDto paymentDto = new PaymentDto
            {
                MethodId = 1,
                UserId = userId,
                WalletId = wallet_id,
                OrderID = sePayWebhookModel.id.ToString(),
                TransactionID = sePayWebhookModel.id.ToString(),
                PlanId = planId > 0 ? planId : null,
                Date = DateTime.Parse(sePayWebhookModel.transactionDate),
                Description = sePayWebhookModel.content,
                Status = "Success",
                Price = sePayWebhookModel.transferAmount
            };
            bool paymentStatus = await _mediator.Send(new AddPaymentPlanCommand(paymentDto, coin));
            return paymentStatus ? "Success" : "Failed";
        }
    }
}
