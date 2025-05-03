using AIMathProject.Application.Dto.EnrollmentDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Enrollment
{
    public record GetCurrentEnrollmentByUserIdQuery(int userId) : IRequest<EnrollmentDto>;

    public class GetCurrentEnrollmentByUserIdHandler : IRequestHandler<GetCurrentEnrollmentByUserIdQuery, EnrollmentDto>
    {
        private readonly IEnrollmentRepository<EnrollmentDto> _repository;

        public GetCurrentEnrollmentByUserIdHandler(IEnrollmentRepository<EnrollmentDto> repository)
        {
            _repository = repository;
        }

        public Task<EnrollmentDto> Handle(GetCurrentEnrollmentByUserIdQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetCurrentEnrollmentByUserId(request.userId);
        }
    }
}