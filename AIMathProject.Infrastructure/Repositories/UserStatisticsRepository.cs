//using AIMathProject.Application.Dto.UserStatisticsDto;
//using AIMathProject.Application.Mappers;
//using AIMathProject.Domain.Entities;
//using AIMathProject.Domain.Interfaces;
//using AIMathProject.Infrastructure.Data;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AIMathProject.Infrastructure.Repositories
//{
//    public class UserStatisticsRepository : IUserStatisticsRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public UserStatisticsRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<int> GetUserCountByPeriod(DateTime startDate, DateTime endDate)
//        {
//            return await _context.UserSessions
//                .Where(s => s.LoginTime >= startDate && s.LoginTime <= endDate)
//                .Select(s => s.UserId)
//                .Distinct()
//                .CountAsync();
//        }

//        public async Task<double> GetUserGrowthRate(DateTime currentPeriodStart, DateTime currentPeriodEnd, DateTime previousPeriodStart, DateTime previousPeriodEnd)
//        {
//            var currentCount = await GetUserCountByPeriod(currentPeriodStart, currentPeriodEnd);
//            var previousCount = await GetUserCountByPeriod(previousPeriodStart, previousPeriodEnd);

//            if (previousCount == 0)
//                return 100; // Nếu kỳ trước không có người dùng, tăng 100%

//            return Math.Round(((double)(currentCount - previousCount) / previousCount) * 100, 2);
//        }

//        public async Task<TimeSpan> GetAverageSessionDuration(DateTime startDate, DateTime endDate)
//        {
//            var sessions = await _context.UserSessions
//                .Where(s => s.LoginTime >= startDate && s.LoginTime <= endDate && s.Duration.HasValue)
//                .Select(s => s.Duration.Value)
//                .ToListAsync();

//            if (!sessions.Any())
//                return TimeSpan.Zero;

//            double totalTicks = sessions.Sum(s => s.Ticks);
//            return TimeSpan.FromTicks((long)(totalTicks / sessions.Count));
//        }

//        public async Task<IEnumerable<UserSession>> GetUserSessionsByPeriod(DateTime startDate, DateTime endDate)
//        {
//            return await _context.UserSessions
//                .Where(s => s.LoginTime >= startDate && s.LoginTime <= endDate)
//                .Include(s => s.User)
//                .ToListAsync();
//        }

//        public async Task<List<(DateTime Date, int UserCount, double AverageMinutes)>> GetDailyStatistics(DateTime startDate, DateTime endDate)
//        {
//            var result = new List<(DateTime Date, int UserCount, double AverageMinutes)>();

//            // Lấy số lượng người dùng theo ngày
//            var dailyCounts = await _context.UserSessions
//                .Where(s => s.LoginTime >= startDate && s.LoginTime <= endDate)
//                .GroupBy(s => s.LoginTime.Date)
//                .Select(g => new { Date = g.Key, Count = g.Select(s => s.UserId).Distinct().Count() })
//                .ToDictionaryAsync(k => k.Date, v => v.Count);

//            var sessionsWithDuration = await _context.UserSessions
//                .Where(s => s.LoginTime >= startDate && s.LoginTime <= endDate && s.Duration.HasValue)
//                .ToListAsync();

//            // Lấy thời gian sử dụng trung bình theo ngày
//            var dailyUsageTimes = sessionsWithDuration
//                .GroupBy(s => s.LoginTime.Date)
//                .ToDictionary(
//                    g => g.Key,
//                    g => Math.Round(g.Average(s => s.Duration.Value.TotalMinutes), 2)
//                );

//            // Lấy tất cả các ngày trong khoảng
//            for (var day = startDate.Date; day <= endDate.Date; day = day.AddDays(1))
//            {
//                int userCount = dailyCounts.ContainsKey(day) ? dailyCounts[day] : 0;
//                double avgMinutes = dailyUsageTimes.ContainsKey(day) ? dailyUsageTimes[day] : 0;

//                result.Add((day, userCount, avgMinutes));
//            }

//            return result;
//        }

//        public async Task StartUserSession(int userId)
//        {
//            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
//            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTimeZone);

//            await _context.UserSessions.AddAsync(new UserSession
//            {
//                UserId = userId,
//                LoginTime = localTime
//            });

//            await _context.SaveChangesAsync();
//        }

//        public async Task EndUserSession(int userId)
//        {
//            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
//            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTimeZone);

//            var session = await _context.UserSessions
//                .Where(s => s.UserId == userId && !s.LogoutTime.HasValue)
//                .OrderByDescending(s => s.LoginTime)
//                .FirstOrDefaultAsync();

//            if (session != null)
//            {
//                session.LogoutTime = localTime;
//                session.Duration = session.LogoutTime.Value - session.LoginTime;
//                await _context.SaveChangesAsync();
//            }
//        }
//    }
//}