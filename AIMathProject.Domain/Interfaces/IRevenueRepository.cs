using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IRevenueRepository<T> where T : class
    {
        Task<T> GetRevenueStatistics(string period);
        Task<List<(DateTime date, decimal revenue)>> GetDailyRevenueByDateRange(DateTime startDate, DateTime endDate);
    }
}
