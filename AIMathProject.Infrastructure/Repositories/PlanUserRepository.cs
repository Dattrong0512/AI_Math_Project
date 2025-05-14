using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class PlanUserRepository : IPlanUserRepository<PlanUser>
    {
        private readonly ApplicationDbContext _context;

        public PlanUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPlanUser(PlanUser user)
        {
            await _context.AddAsync(user);
            int row = await _context.SaveChangesAsync();
            return row > 0 ? true : false;
        }
    }
}
