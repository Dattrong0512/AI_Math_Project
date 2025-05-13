using AIMathProject.Application.Dto.Payment.PlanDto;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Mappers.PaymentServices
{
    public static class PlansMapper
    {
        public static PlansDto ToPlansDto(this Plan plan)
        {
            PlansDto dto = new PlansDto
            {
                PlanId  = plan.PlanId,

                PlanName = plan.PlanName,

                 Price = plan.Price,

                 Coins = plan.Coins,

                 Description = plan.Description
            };
            return dto;

        }

    }
}
