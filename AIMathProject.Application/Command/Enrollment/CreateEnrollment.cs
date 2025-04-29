using AIMathProject.Application.Dto.EnrollmentDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.Enrollment
{
    public record CreateEnrollmentCommand(EnrollmentDto EnrollmentDto) : IRequest<EnrollmentDto>;

    public class CreateEnrollmentCommandHandler : IRequestHandler<CreateEnrollmentCommand, EnrollmentDto>
    {
        private readonly IEnrollmentRepository<EnrollmentDto> _enrollmentRepository;

        public CreateEnrollmentCommandHandler(IEnrollmentRepository<EnrollmentDto> enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<EnrollmentDto> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            return await _enrollmentRepository.CreateEnrollment(request.EnrollmentDto);
        }
    }
}