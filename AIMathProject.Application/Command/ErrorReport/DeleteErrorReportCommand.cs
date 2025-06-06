using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Commands.ErrorReport
{
    public class DeleteErrorReportCommand : IRequest<bool>
    {
        public int ErrorId { get; }

        public DeleteErrorReportCommand(int errorId)
        {
            ErrorId = errorId;
        }
    }

    public class DeleteErrorReportCommandHandler : IRequestHandler<DeleteErrorReportCommand, bool>
    {
        private readonly IErrorReportRepository<AIMathProject.Application.Dto.ErrorReportDto.ErrorReportDto> _repository;

        public DeleteErrorReportCommandHandler(IErrorReportRepository<AIMathProject.Application.Dto.ErrorReportDto.ErrorReportDto> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteErrorReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.DeleteErrorReport(request.ErrorId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error deleting error report: {ex.Message}");
            }
        }
    }
}