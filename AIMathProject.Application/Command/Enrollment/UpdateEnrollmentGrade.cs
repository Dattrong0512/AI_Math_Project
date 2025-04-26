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
    public record UpdateEnrollmentGradeCommand(int enrollmentid, short newgrade) : IRequest<EnrollmentDto>;

    public class UpdateEnrollmentGradeHandler(IEnrollmentRepository<EnrollmentDto> repository)
        : IRequestHandler<UpdateEnrollmentGradeCommand, EnrollmentDto>
    {
        public Task<EnrollmentDto> Handle(UpdateEnrollmentGradeCommand request, CancellationToken cancellationToken)
        {
            return repository.UpdateEnrollmentGrade(request.enrollmentid, request.newgrade);
        }
    }
}
