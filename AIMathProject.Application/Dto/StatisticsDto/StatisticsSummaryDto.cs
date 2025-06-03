using System;

namespace AIMathProject.Application.Dto.StatisticsDto
{
    public class StatisticsSummaryDto
    {
        public UserCountStatisticsDto? UserCounts { get; set; }
        public UserEngagementStatisticsDto? UserEngagement { get; set; }
        public List<UserDailyStatisticsDto>? DailyStatistics { get; set; }

        public RevenueStatisticsDto? RevenueStatistics { get; set; }
    }
}