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
        Task<List<T>> GetAllPayment(int userID);
        Task<T> GetLatestPayment(int userID);
        Task<(List<T> items, int totalCount, int pageIndex, int pageSize)> GetAllPaymentsPaginated(int pageIndex, int pageSize);
    }
}
