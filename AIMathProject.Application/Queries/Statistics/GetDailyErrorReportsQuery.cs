using AIMathProject.Application.Dto.StatisticsDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Statistics
{
    public class GetDailyErrorReportsQuery : IRequest<List<DailyErrorReportDto>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetDailyErrorReportsQueryHandler : IRequestHandler<GetDailyErrorReportsQuery, List<DailyErrorReportDto>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetDailyErrorReportsQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<List<DailyErrorReportDto>> Handle(GetDailyErrorReportsQuery request, CancellationToken cancellationToken)
        {
            var errorReports = await _statisticsRepository.GetDailyErrorReportsByDateRange(request.StartDate, request.EndDate);

            return errorReports.Select(e => new DailyErrorReportDto
            {
                Date = e.date,
                ErrorCount = e.totalErrors,
                ResolvedCount = e.resolvedErrors,
                UnresolvedCount = e.unresolvedErrors
            }).ToList();
        }
    }
}