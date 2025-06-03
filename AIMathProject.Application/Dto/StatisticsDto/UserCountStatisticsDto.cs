

namespace AIMathProject.Application.Dto.StatisticsDto
{
    public class UserCountStatisticsDto
    {
        public int CurrentCount { get; set; }
        public int PreviousCount { get; set; }
        public double GrowthRate { get; set; } // Phần trăm tăng/giảm
        public PeriodStatisticsDto? Period { get; set; }
    }
}