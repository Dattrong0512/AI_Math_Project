using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface ITokenPackageRepository<T> where T : class
    {
        Task<T> GetInfoTokenPackageById(int id);
    }
}
