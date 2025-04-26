using System;

namespace AIMathProject.Application.Dto.UserStatisticsDto
{
    public class UserStatisticsSummaryDto
    {
        public UserCountStatisticsDto? UserCounts { get; set; }
        public UserEngagementStatisticsDto? UserEngagement { get; set; }
        public List<UserDailyStatisticsDto>? DailyStatistics { get; set; }
    }
}