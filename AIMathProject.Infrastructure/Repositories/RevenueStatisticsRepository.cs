using AIMathProject.Application.Dto.RevenueStatisticsDto;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class RevenueStatisticsRepository : IRevenueRepository<RevenueStatisticsDto> 
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RevenueStatisticsRepository> _logger;


        public RevenueStatisticsRepository(ApplicationDbContext context, ILogger<RevenueStatisticsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<RevenueStatisticsDto> GetRevenueStatistics(string period)
        {
            DateTime currentPeriodEnd = DateTime.Now;
            DateTime currentPeriodStart;
            DateTime previousPeriodStart;
            DateTime previousPeriodEnd;
            string periodName;

            // Set date ranges based on period
            switch (period.ToLower())
            {
                case "day":
                    currentPeriodStart = currentPeriodEnd.Date;
                    previousPeriodStart = currentPeriodStart.AddDays(-1);
                    previousPeriodEnd = previousPeriodStart.AddDays(1).AddSeconds(-1);
                    periodName = "Daily";
                    break;
                case "week":
                    // Start from current week beginning (assuming Monday as first day)
                    int daysToSubtract = ((int)currentPeriodEnd.DayOfWeek == 0 ? 7 : (int)currentPeriodEnd.DayOfWeek) - 1;
                    currentPeriodStart = currentPeriodEnd.Date.AddDays(-daysToSubtract);
                    previousPeriodStart = currentPeriodStart.AddDays(-7);
                    previousPeriodEnd = currentPeriodStart.AddSeconds(-1);
                    periodName = "Weekly";
                    break;
                case "year":
                    currentPeriodStart = new DateTime(currentPeriodEnd.Year, 1, 1);
                    previousPeriodStart = new DateTime(currentPeriodEnd.Year - 1, 1, 1);
                    previousPeriodEnd = new DateTime(currentPeriodEnd.Year - 1, 12, 31, 23, 59, 59);
                    periodName = "Yearly";
                    break;
                default: // month
                    currentPeriodStart = new DateTime(currentPeriodEnd.Year, currentPeriodEnd.Month, 1);
                    if (currentPeriodEnd.Month == 1)
                    {
                        previousPeriodStart = new DateTime(currentPeriodEnd.Year - 1, 12, 1);
                        previousPeriodEnd = new DateTime(currentPeriodEnd.Year - 1, 12, 31, 23, 59, 59);
                    }
                    else
                    {
                        previousPeriodStart = new DateTime(currentPeriodEnd.Year, currentPeriodEnd.Month - 1, 1);
                        previousPeriodEnd = currentPeriodStart.AddSeconds(-1);
                    }
                    periodName = "Monthly";
                    break;
            }

            // Get current period revenue
            decimal? currentRevenue = await _context.Payments
                .Where(p => p.Date >= currentPeriodStart && p.Date <= currentPeriodEnd && p.Status == "Success")
                .SumAsync(p => p.Price);

            // Get previous period revenue
            decimal? previousRevenue = await _context.Payments
                .Where(p => p.Date >= previousPeriodStart && p.Date <= previousPeriodEnd && p.Status == "Success")
                .SumAsync(p => p.Price);

            // Calculate growth rate
            double growthRate = 0;
            if (previousRevenue > 0)
            {
                growthRate = Math.Round(((double)(currentRevenue - previousRevenue) / (double)previousRevenue) * 100, 2);
            }
            else if (currentRevenue > 0)
            {
                growthRate = 100; 
            }

            // Create response using DTOs
            var periodDto = new PeriodRevenueStatisticsDto
            {
                StartDate = currentPeriodStart,
                EndDate = currentPeriodEnd,
                PeriodType = periodName
            };

            return new RevenueStatisticsDto
            {
                CurrentRevenue = (decimal)currentRevenue,
                PreviousRevenue = (decimal)previousRevenue,
                GrowthRate = growthRate,
                Period = periodDto
            };
        }

        public async Task<List<(DateTime date, decimal revenue)>> GetDailyRevenueByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var payments = await (
                    from p in _context.Payments
                    where p.Date >= startDate && p.Date <= endDate && p.Status == "Success"
                    select new
                    {
                        Date = p.Date,
                        Revenue = p.Price
                    }
                ).ToListAsync();

                var groupedRevenues = payments
                    .Where(p => p.Date.HasValue)
                    .GroupBy(p => p.Date.Value.Date) 
                    .Select(g => new
                    {
                        Date = g.Key,
                        Revenue = g.Sum(x => x.Revenue)
                    })
                    .OrderBy(dr => dr.Date)
                    .ToList();

                return groupedRevenues.Select(dr => (dr.Date, (decimal)dr.Revenue)).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
