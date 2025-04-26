

namespace AIMathProject.Application.Dto.UserStatisticsDto
{
    public class UserCountStatisticsDto
    {
        public int CurrentCount { get; set; }
        public int PreviousCount { get; set; }
        public double GrowthRate { get; set; } // Phần trăm tăng/giảm
        public PeriodUserStatisticsDto? Period { get; set; }
    }
}