using AIMathProject.Application.Dto.ErrorReportDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.ErrorReport
{
    public record GetErrorReportByIdQuery(int ErrorId) : IRequest<ErrorReportDto>;

    public class GetErrorReportByIdQueryHandler : IRequestHandler<GetErrorReportByIdQuery, ErrorReportDto>
    {
        private readonly IErrorReportRepository<ErrorReportDto> _repository;

        public GetErrorReportByIdQueryHandler(IErrorReportRepository<ErrorReportDto> repository)
        {
            _repository = repository;
        }

        public async Task<ErrorReportDto> Handle(GetErrorReportByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetErrorReportById(request.ErrorId);
        }
    }
}