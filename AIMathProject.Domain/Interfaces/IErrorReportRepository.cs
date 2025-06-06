using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IErrorReportRepository<T> where T : class
    {
        Task<T> CreateErrorReport(int userId, string errorMessage);
        Task<T> GetErrorReportById(int errorId);
        Task<List<T>> GetErrorReportsByUserId(int userId);
        Task<List<T>> GetAllErrorReports();
        Task<List<T>> GetErrorReports(
            string searchTerm = null,
            string errorType = null,
            bool? resolved = null,
            int pageNumber = 1,
            int pageSize = 10,
            bool newestFirst = true);
        Task<int> GetTotalErrorReportsCount(
            string searchTerm = null,
            string errorType = null,
            bool? resolved = null);
        Task<T> ResolveErrorReport(int errorId);
        Task<bool> DeleteErrorReport(int errorId);
    }
}