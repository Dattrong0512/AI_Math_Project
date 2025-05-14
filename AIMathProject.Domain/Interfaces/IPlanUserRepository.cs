using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IPlanUserRepository<T> where T : class
    {
        Task<bool> AddPlanUser(T user);
    }
}
