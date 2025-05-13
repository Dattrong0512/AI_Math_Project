using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Application.Dto.Payment.PlanDto;
using AIMathProject.Application.Dto.Payment.TokenPackageDto;
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

                UserId = payment.UserId,

                TokenPackageId = payment.TokenPackageId,

                PlanId = payment.PlanId,

                Date = payment.Date,

                Description = payment.Description,

                Status = payment.Status,

                Price = payment.Price,
                OrderID = payment.OrderID,
                TransactionID = payment.TransactionID,

                Method = payment.Method,

                Plan = payment.Plan.ToPlansDto(),

                TokenPackage = payment.TokenPackage.ToTokenPackageDto()
            };
            return dto;
        }
        public static Payment ToPayment(this PaymentDto dto)
        {
            Payment payment = new Payment
            {

                MethodId = dto.MethodId,

                UserId = dto.UserId,

                TokenPackageId = dto.TokenPackageId,

                PlanId = dto.PlanId,

                Date = dto.Date,

                Description = dto.Description,

                Status = dto.Status,

                Price = dto.Price,
                OrderID = dto.OrderID,
                TransactionID = dto.TransactionID,

                Method = dto.Method,

                Plan = null,

                TokenPackage = null
            };
            return payment;
        }


    }
}
