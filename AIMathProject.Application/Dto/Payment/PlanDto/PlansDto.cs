using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.Payment.PlanDto
{
    public class PlansDto
    {
        public int PlanId { get; set; }

        public string PlanName { get; set; } = null!;

        public decimal? Price { get; set; }

        public int? Coins { get; set; }

        public string? Description { get; set; }

    }
}
