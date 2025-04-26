using System;

namespace AIMathProject.Application.Dto.UserStatisticsDto
{
    public class PeriodUserStatisticsDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? PeriodType { get; set; } // "day", "week", "month", "year"
    }
}