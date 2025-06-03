namespace AIMathProject.Application.Dto.StatisticsDto
{
    public class UserEngagementStatisticsDto
    {
        public double CurrentAverageMinutes { get; set; }
        public double PreviousAverageMinutes { get; set; }
        public double ChangeRate { get; set; } // Phần trăm thay đổi
        public PeriodStatisticsDto? Period { get; set; }
    }
}