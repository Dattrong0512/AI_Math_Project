using System;

namespace AIMathProject.Application.Dto.StatisticsDto
{
    public class UserDailyStatisticsDto
    {
        public DateTime Date { get; set; }
        public int UserCount { get; set; }
        public double AverageUsageMinutes { get; set; }
    }
}