using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IStatisticsRepository
    {
        Task<int> GetUserCountByPeriod(DateTime startDate, DateTime endDate);
        Task<double> GetUserGrowthRate(DateTime currentPeriodStart, DateTime currentPeriodEnd, DateTime previousPeriodStart, DateTime previousPeriodEnd);
        Task<TimeSpan> GetAverageSessionDuration(DateTime startDate, DateTime endDate);

        Task<IEnumerable<UserSession>> GetUserSessionsByPeriod(DateTime startDate, DateTime endDate);
        Task<List<(DateTime Date, int UserCount, double AverageMinutes)>> GetDailyStatistics(DateTime startDate, DateTime endDate);
        Task<decimal?> GetRevenueByPeriod(DateTime startDate, DateTime endDate);
        Task<double> GetRevenueGrowthRate(DateTime currentPeriodStart, DateTime currentPeriodEnd, DateTime previousPeriodStart, DateTime previousPeriodEnd);
        Task<int> GetErrorCountByPeriod(DateTime startDate, DateTime endDate);
        Task<double> GetErrorGrowthRate(DateTime currentPeriodStart, DateTime currentPeriodEnd, DateTime previousPeriodStart, DateTime previousPeriodEnd);
        Task<List<(DateTime date, int totalErrors, int resolvedErrors, int unresolvedErrors)>> GetDailyErrorReportsByDateRange(DateTime startDate, DateTime endDate);
        Task<List<(DateTime date, decimal revenue)>> GetDailyRevenueByDateRange(DateTime startDate, DateTime endDate);

        Task StartUserSession(int userId);
        Task EndUserSession(int userId);
    }
}