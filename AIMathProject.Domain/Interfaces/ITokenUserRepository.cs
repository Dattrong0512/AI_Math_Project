using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface ITokenUserRepository<T> where T : class
    {

        Task<bool> AddTokenUser(T TkUser);
    }
}
