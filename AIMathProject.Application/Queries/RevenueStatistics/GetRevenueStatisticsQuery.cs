using AIMathProject.Application.Dto.RevenueStatisticsDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.RevenueStatistics
{
    public record GetRevenueStatisticsQuery(string PeriodType) : IRequest<RevenueStatisticsDto>;

    public class GetRevenueStatisticsQueryHandler : IRequestHandler<GetRevenueStatisticsQuery, RevenueStatisticsDto>
    {
        private readonly IRevenueRepository<RevenueStatisticsDto> _repository;

        public GetRevenueStatisticsQueryHandler(IRevenueRepository<RevenueStatisticsDto> repository)
        {
            _repository = repository;
        }

        public async Task<RevenueStatisticsDto> Handle(GetRevenueStatisticsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetRevenueStatistics(request.PeriodType);
            return result;
        }
    }
}