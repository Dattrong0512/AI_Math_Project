using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.RevenueStatisticsDto
{
    public class RevenueStatisticsDto
    {
        public decimal CurrentRevenue { get; set; }
        public decimal PreviousRevenue { get; set; }
        public double GrowthRate { get; set; }
        public PeriodRevenueStatisticsDto? Period { get; set; }
    }
}
