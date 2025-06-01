using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.RevenueStatisticsDto
{
    public class PeriodRevenueStatisticsDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? PeriodType { get; set; } // "day", "week", "month", "year"
    }
}
