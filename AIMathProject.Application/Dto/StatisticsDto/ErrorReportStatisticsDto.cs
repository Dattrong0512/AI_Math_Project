using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.StatisticsDto
{
    public class ErrorReportStatisticsDto
    {
        public int CurrentCount { get; set; }
        public int PreviousCount { get; set; }
        public double GrowthRate { get; set; } // Phần trăm tăng/giảm
        public PeriodStatisticsDto? Period { get; set; }
    }
}