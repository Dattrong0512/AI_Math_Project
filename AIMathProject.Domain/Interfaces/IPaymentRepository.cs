using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IPaymentRepository<T> where T : class
    {
        Task<bool> AddPayment(T payment);
        Task<T> GetLatestInfoPaymentUserById(int id);

        Task<ICollection<T>> GetAllInfoPaymentUserById(int id);
    }
}
