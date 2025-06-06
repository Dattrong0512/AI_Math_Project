using AIMathProject.Application.Dto.ErrorReportDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Commands.ErrorReport
{
    public class ResolveErrorReportCommand : IRequest<ErrorReportDto>
    {
        public int ErrorId { get; }

        public ResolveErrorReportCommand(int errorId)
        {
            ErrorId = errorId;
        }
    }

    public class ResolveErrorReportCommandHandler : IRequestHandler<ResolveErrorReportCommand, ErrorReportDto>
    {
        private readonly IErrorReportRepository<ErrorReportDto> _repository;

        public ResolveErrorReportCommandHandler(IErrorReportRepository<ErrorReportDto> repository)
        {
            _repository = repository;
        }

        public async Task<ErrorReportDto> Handle(ResolveErrorReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.ResolveErrorReport(request.ErrorId);
            }
            catch (ArgumentException ex)
            {
                throw new ApplicationException($"Error resolving error report: {ex.Message}");
            }
        }
    }
}