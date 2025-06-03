using System;

namespace AIMathProject.Application.Dto.StatisticsDto
{
    public class PeriodStatisticsDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? PeriodType { get; set; } // "day", "week", "month", "year"
    }
}