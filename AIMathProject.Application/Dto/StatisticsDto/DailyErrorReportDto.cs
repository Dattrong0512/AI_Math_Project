using System;

namespace AIMathProject.Application.Dto.StatisticsDto
{
    public class DailyErrorReportDto
    {
        public DateTime Date { get; set; }
        public int ErrorCount { get; set; }
        public int ResolvedCount { get; set; } // Số lượng lỗi đã giải quyết
        public int UnresolvedCount { get; set; } // Số lượng lỗi chưa giải quyết
    }
}