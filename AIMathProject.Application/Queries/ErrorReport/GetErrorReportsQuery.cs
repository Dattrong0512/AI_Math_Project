using AIMathProject.Application.Dto.ErrorReportDto;
using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.ErrorReport
{
    public class GetErrorReportsQuery : IRequest<Pagination<ErrorReportDto>>
    {
        public string SearchTerm { get; }
        public string ErrorType { get; }
        public bool? Resolved { get; }
        public int PageIndex { get; }
        public int PageSize { get; }

        public GetErrorReportsQuery(
            string searchTerm = null,
            string errorType = null,
            bool? resolved = null,
            int pageIndex = 0,
            int pageSize = 10)
        {
            SearchTerm = searchTerm;
            ErrorType = errorType;
            Resolved = resolved;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    public class GetErrorReportsQueryHandler : IRequestHandler<GetErrorReportsQuery, Pagination<ErrorReportDto>>
    {
        private readonly IErrorReportRepository<ErrorReportDto> _repository;

        public GetErrorReportsQueryHandler(IErrorReportRepository<ErrorReportDto> repository)
        {
            _repository = repository;
        }

        public async Task<Pagination<ErrorReportDto>> Handle(GetErrorReportsQuery request, CancellationToken cancellationToken)
        {
            var errorReports = await _repository.GetErrorReports(
                request.SearchTerm,
                request.ErrorType,
                request.Resolved,
                request.PageIndex + 1,
                request.PageSize,
                true);

            var totalCount = await _repository.GetTotalErrorReportsCount(
                request.SearchTerm,
                request.ErrorType,
                request.Resolved);

            return new Pagination<ErrorReportDto>
            {
                Items = errorReports,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }
    }
}