using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IUserStatisticsRepository
    {
        Task<int> GetUserCountByPeriod(DateTime startDate, DateTime endDate);
        Task<double> GetUserGrowthRate(DateTime currentPeriodStart, DateTime currentPeriodEnd, DateTime previousPeriodStart, DateTime previousPeriodEnd);
        Task<TimeSpan> GetAverageSessionDuration(DateTime startDate, DateTime endDate);

        Task<IEnumerable<UserSession>> GetUserSessionsByPeriod(DateTime startDate, DateTime endDate);
        Task<List<(DateTime Date, int UserCount, double AverageMinutes)>> GetDailyStatistics(DateTime startDate, DateTime endDate);

        Task StartUserSession(int userId);
        Task EndUserSession(int userId);
    }
}