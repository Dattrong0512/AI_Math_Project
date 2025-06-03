using AIMathProject.Application.Dto.RevenueStatisticsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.StatisticsDto
{
    public class RevenueStatisticsDto
    {
        public decimal CurrentRevenue { get; set; }
        public decimal PreviousRevenue { get; set; }
        public double GrowthRate { get; set; }
        public PeriodStatisticsDto? Period { get; set; }
    }
}
