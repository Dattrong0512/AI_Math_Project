using AIMathProject.Application.Dto.EnrollmentDto;
using AIMathProject.Application.Dto.LessonProgressDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.LessonProgress
{
    public record UpdateEnrollmentCommand(EnrollmentDto EnrollmentDto) : IRequest<EnrollmentDto>;

    public class UpdateEnrollmentCommandHandler : IRequestHandler<UpdateEnrollmentCommand, EnrollmentDto>
    {
        private readonly IEnrollmentRepository<EnrollmentDto> _repository;

        public UpdateEnrollmentCommandHandler(IEnrollmentRepository<EnrollmentDto> repository)
        {
            _repository = repository;
        }

        public async Task<EnrollmentDto> Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateEnrollment(request.EnrollmentDto);
        }
    }

}
