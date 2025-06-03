using AIMathProject.Application.Dto.StatisticsDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.UserStatistics
{
    public record GetDailyRevenueByDateRangeQuery(DateTime StartDate, DateTime EndDate) : IRequest<List<DailyRevenueDto>>;

    public class GetDailyRevenueByDateRangeQueryHandler : IRequestHandler<GetDailyRevenueByDateRangeQuery, List<DailyRevenueDto>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetDailyRevenueByDateRangeQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<List<DailyRevenueDto>> Handle(GetDailyRevenueByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var tupleResults = await _statisticsRepository.GetDailyRevenueByDateRange(request.StartDate, request.EndDate);

            return tupleResults.Select(tuple => new DailyRevenueDto
            {
                Date = tuple.date,
                Revenue = tuple.revenue
            }).ToList();
        }
    }
}