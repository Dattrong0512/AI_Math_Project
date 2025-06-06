using AIMathProject.Application.Dto.ErrorReportDto;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Domain.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.ErrorReport
{
    public record CreateErrorReportCommand(int UserId, string ErrorMessage) : IRequest<ErrorReportDto>;

    public class CreateErrorReportCommandHandler : IRequestHandler<CreateErrorReportCommand, ErrorReportDto>
    {
        private readonly IErrorReportRepository<ErrorReportDto> _repository;

        public CreateErrorReportCommandHandler(IErrorReportRepository<ErrorReportDto> repository)
        {
            _repository = repository;
        }

        public Task<ErrorReportDto> Handle(CreateErrorReportCommand request, CancellationToken cancellationToken)
        {
            return _repository.CreateErrorReport(request.UserId, request.ErrorMessage);
        }
    }
}