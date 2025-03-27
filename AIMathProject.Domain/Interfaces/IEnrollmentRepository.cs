using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IEnrollmentRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllEnrollmentByID(int id);

    }
}
