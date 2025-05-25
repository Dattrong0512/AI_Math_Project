using AIMathProject.Application.Dto.UserStatisticsDto;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIMathProject.Application.Mappers
{
    public static class UserSessionMappers
    {
        public static UserSessionDto ToUserSessionDto(this UserSession session)
        {
            if (session == null) return null;

            return new UserSessionDto
            {
                UserSessionId = session.UserSessionId,
                UserId = session.UserId,
                Username = session.User?.UserName ?? "Unknown",
                LoginTime = session.LoginTime,
                LogoutTime = session.LogoutTime,
                DurationMinutes = session.Duration.HasValue ? Math.Round(session.Duration.Value.TotalMinutes, 2) : 0
            };
        }

        public static List<UserSessionDto> ToUserSessionDtoList(this IEnumerable<UserSession> sessions)
        {
            if (sessions == null) return new List<UserSessionDto>();

            return sessions.Select(s => s.ToUserSessionDto()).ToList();
        }

        public static UserDailyStatisticsDto ToUserDailyStatisticsDto(this DateTime date, int userCount, double averageMinutes)
        {
            return new UserDailyStatisticsDto
            {
                Date = date,
                UserCount = userCount,
                AverageUsageMinutes = averageMinutes
            };
        }

        public static PeriodUserStatisticsDto ToPeriodStatisticsDto(
            this (DateTime startDate, DateTime endDate, string periodType) periodInfo)
        {
            return new PeriodUserStatisticsDto
            {
                StartDate = periodInfo.startDate,
                EndDate = periodInfo.endDate,
                PeriodType = periodInfo.periodType
            };
        }
    }
}