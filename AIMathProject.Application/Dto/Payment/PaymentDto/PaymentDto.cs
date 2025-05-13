using AIMathProject.Application.Dto.Payment.PlanDto;
using AIMathProject.Application.Dto.Payment.TokenPackageDto;
using AIMathProject.Application.Dto.UserDto;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.Payment.PaymentDto
{
    public class PaymentDto
    {
            public int PaymentId { get; set; }

            public int MethodId { get; set; }

            public int UserId { get; set; }

            public int? TokenPackageId { get; set; }

            public int? PlanId { get; set; }

            public DateTime? Date { get; set; }

            public string? Description { get; set; }

            public bool? Status { get; set; }

            public decimal? Price { get; set; }

            public string? OrderID { get; set; }
            public string? TransactionID { get; set; }

            public  PaymentMethod Method { get; set; } = null!;

            public  PlansDto? Plan { get; set; }

            public TokenPackageDto.TokenPackageDto? TokenPackage { get; set; }

    }
}
