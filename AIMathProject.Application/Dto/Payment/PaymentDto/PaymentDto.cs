using AIMathProject.Application.Dto.Payment.PlanDto;


namespace AIMathProject.Application.Dto.Payment.PaymentDto
{
    public class PaymentDto
    {
            public int? PaymentId { get; set; }

            public int MethodId { get; set; }

            public int UserId { get; set; }

            public int WalletId { get; set; }

            public string? OrderID { get; set; }
            public string? TransactionID { get; set; }

            public int? PlanId { get; set; }

            public DateTime? Date { get; set; }

            public string? Description { get; set; }

            public string? Status { get; set; }

            public decimal? Price { get; set; }


            public MethodDto.MethodDto Method { get; set; }
    
            public  PlansDto? Plan { get; set; }

    }
}
