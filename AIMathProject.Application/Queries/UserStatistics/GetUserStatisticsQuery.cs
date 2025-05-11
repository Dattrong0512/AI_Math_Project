//using AIMathProject.Application.Abstracts;
//using AIMathProject.Application.Dto.UserDto;
//using AIMathProject.Application.Dto.UserStatisticsDto;
//using AIMathProject.Application.Mappers;
//using AIMathProject.Domain.Interfaces;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace AIMathProject.Application.Queries.UserStatistics
//{
//    public record GetUserStatisticsQuery(string PeriodType) : IRequest<UserStatisticsSummaryDto>;

//    public class GetUserStatisticsQueryHandler : IRequestHandler<GetUserStatisticsQuery, UserStatisticsSummaryDto>
//    {
//        private readonly IUserStatisticsRepository _repository;

//        public GetUserStatisticsQueryHandler(IUserStatisticsRepository repository)
//        {
//            _repository = repository;
//        }

//        public async Task<UserStatisticsSummaryDto> Handle(GetUserStatisticsQuery request, CancellationToken cancellationToken)
//        {

//            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
//            var nowUtc = DateTime.UtcNow;
//            var now = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, vnTimeZone);

//            var dateRanges = GetDateRanges(request.PeriodType, now);

//            var (currentStart, currentEnd, previousStart, previousEnd) = dateRanges;

//            // Tạo đối tượng Period
//            var period = (currentStart, currentEnd, request.PeriodType).ToPeriodStatisticsDto();

//            // Lấy thông tin về số lượng người dùng
//            var currentUserCount = await _repository.GetUserCountByPeriod(currentStart, currentEnd);
//            var previousUserCount = await _repository.GetUserCountByPeriod(previousStart, previousEnd);
//            var userGrowthRate = await _repository.GetUserGrowthRate(currentStart, currentEnd, previousStart, previousEnd);

//            // Lấy thông tin về thời gian sử dụng
//            var currentAvgDuration = await _repository.GetAverageSessionDuration(currentStart, currentEnd);
//            var previousAvgDuration = await _repository.GetAverageSessionDuration(previousStart, previousEnd);

//            double currentAvgMinutes = currentAvgDuration.TotalMinutes;
//            double previousAvgMinutes = previousAvgDuration.TotalMinutes;
//            double durationChangeRate = 0;

//            if (previousAvgMinutes > 0)
//            {
//                durationChangeRate = Math.Round(((currentAvgMinutes - previousAvgMinutes) / previousAvgMinutes) * 100, 2);
//            }

//            // Lấy thống kê chi tiết theo ngày
//            var dailyStats = await _repository.GetDailyStatistics(currentStart, currentEnd);

//            var result = new UserStatisticsSummaryDto
//            {
//                UserCounts = new UserCountStatisticsDto
//                {
//                    CurrentCount = currentUserCount,
//                    PreviousCount = previousUserCount,
//                    GrowthRate = userGrowthRate,
//                    Period = period
//                },
//                UserEngagement = new UserEngagementStatisticsDto
//                {
//                    CurrentAverageMinutes = Math.Round(currentAvgMinutes, 2),
//                    PreviousAverageMinutes = Math.Round(previousAvgMinutes, 2),
//                    ChangeRate = durationChangeRate,
//                    Period = period
//                },

//                DailyStatistics = dailyStats.Select(d => new UserDailyStatisticsDto
//                {
//                    Date = d.Date,
//                    UserCount = d.UserCount,
//                    AverageUsageMinutes = d.AverageMinutes
//                }).ToList()
//            };

//            return result;
//        }

//        private (DateTime currentStart, DateTime currentEnd, DateTime previousStart, DateTime previousEnd)
//            GetDateRanges(string periodType, DateTime now)
//        {
//            DateTime currentStart, currentEnd, previousStart, previousEnd;

//            switch (periodType.ToLower())
//            {
//                case "day":
//                    currentStart = now.Date;
//                    currentEnd = now;
//                    previousStart = currentStart.AddDays(-1);
//                    previousEnd = currentStart.AddMilliseconds(-1);
//                    break;

//                case "week":
//                    // Lấy ngày đầu tuần
//                    int daysToSubtract = (int)now.DayOfWeek;
//                    currentStart = now.Date.AddDays(-daysToSubtract);
//                    currentEnd = now;
//                    previousStart = currentStart.AddDays(-7);
//                    previousEnd = currentStart.AddMilliseconds(-1);
//                    break;

//                case "month":
//                    currentStart = new DateTime(now.Year, now.Month, 1);
//                    currentEnd = now;
//                    previousStart = currentStart.AddMonths(-1);
//                    previousEnd = currentStart.AddMilliseconds(-1);
//                    break;

//                case "year":
//                    currentStart = new DateTime(now.Year, 1, 1);
//                    currentEnd = now;
//                    previousStart = currentStart.AddYears(-1);
//                    previousEnd = currentStart.AddMilliseconds(-1);
//                    break;

//                default:
//                    throw new ArgumentException("Invalid period type. Use 'day', 'week', 'month', or 'year'");
//            }

//            return (currentStart, currentEnd, previousStart, previousEnd);
//        }
//    }
//}