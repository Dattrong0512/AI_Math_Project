using AIMathProject.Application.Dto.Payment.MethodDto;
using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Application.Dto.Payment.PlanDto;
using AIMathProject.Application.Dto.Payment.TokenPackage;
using  AIMathProject.Domain.Entities;


namespace AIMathProject.Application.Mappers.PaymentServices
{
    public static class PaymentMapper
    {
        public static PaymentDto ToPaymentDto(this Payment payment)
        {
            PaymentDto dto = new PaymentDto
            {
                PaymentId = payment.PaymentId,
                MethodId = payment.MethodId,
                WalletId = payment.WalletId,
                UserId = payment.Wallet.UserId,
                PlanId = payment.PlanId,
                Date = payment.Date,
                Description = payment.Description,
                Status = payment.Status,
                Price = payment.Price,
                OrderID = payment.OrderId,
                TransactionID = payment.TransactionId,
                Method = payment.Method !=null ? payment.Method.ToMethodDto() : null,
                Plan = payment.Plan != null ? payment.Plan.ToPlansDto() : null,
            };
            return dto;
        }

        public static List<PaymentDto> ToListPaymentDto(ICollection<Payment> list)
        {
            List<PaymentDto> dto = new List<PaymentDto>();
            foreach(var item in list)
            {
                dto.Add(item.ToPaymentDto());
            }
            return dto;
        }
        public static Payment ToPayment(this PaymentDto dto)
        {
            Payment payment = new Payment
            {
                MethodId = dto.MethodId,
                WalletId = dto.WalletId,
                PlanId = dto.PlanId,
                Date = dto.Date,
                Description = dto.Description,
                Status = dto.Status,
                Price = dto.Price,
                OrderId = dto.OrderID,
                TransactionId = dto.TransactionID,
                Method = null,
                Plan = null,
            };
            return payment;
         }


    }
}
