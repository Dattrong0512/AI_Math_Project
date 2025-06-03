using System;

namespace AIMathProject.Application.Dto.StatisticsDto
{
    public class UserSessionDto
    {
        public int UserSessionId { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public double DurationMinutes { get; set; }
    }
}