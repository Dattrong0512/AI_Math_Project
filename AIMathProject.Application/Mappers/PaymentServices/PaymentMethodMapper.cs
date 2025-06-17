using AIMathProject.Application.Dto.Payment.MethodDto;
using AIMathProject.Application.Dto.Payment.PlanDto;
using AIMathProject.Domain.Entities;

namespace AIMathProject.Application.Mappers.PaymentServices
{
    public static class PaymentMethodMapper
    {
        public static MethodDto ToMethodDto(this PaymentMethod method)
        {
            MethodDto dto = new MethodDto
            {
                MethodId = method.MethodId,
                MethodName = method.MethodName,
                MethodIcon = method.MethodIcon
            };
            return dto;
        }
        public static ICollection<MethodDto> ToListPaymentMethodDto(this ICollection<PaymentMethod> list)
        {
            List<MethodDto> dto = new List<MethodDto>();
            foreach (var pl in list)
            {
                dto.Add(pl.ToMethodDto());
            }
            return dto;
        }
    };
}
