using AIMathProject.Application.Dto.Payment.PlanDto;
using AIMathProject.Application.Mappers.PaymentServices;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class PlanRepository : IPlanRepository<PlansDto>
    {
        private readonly ApplicationDbContext _context;

        public PlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PlansDto> GetInfoPlanByID(int id)
        {
            Plan plan = await _context.Plans.FirstOrDefaultAsync(pl => pl.PlanId == id);

            if(plan != null)
            {
                PlansDto dto = plan.ToPlansDto();
                return dto;
            }
            return null;

        }
        public async Task<ICollection<PlansDto>> GetAllPlan()
        {
            List<Plan> listPl = await _context.Plans.ToListAsync();

            return listPl.ToListPlansDto();
        }

    }


}
