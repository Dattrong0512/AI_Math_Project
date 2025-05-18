using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IPlanRepository<T> where T : class
    {
        Task<T> GetInfoPlanByID(int id);

        Task<ICollection<T>> GetAllPlan();
    }
}
