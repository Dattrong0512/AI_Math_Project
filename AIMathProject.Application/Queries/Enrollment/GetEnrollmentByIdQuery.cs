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
    public record GetEnrollmentByIdQuery(int id) : IRequest<ICollection<EnrollmentDto>>;

    public class GetEnrollmentByIdHandler(IEnrollmentRepository<EnrollmentDto> repository)
        : IRequestHandler<GetEnrollmentByIdQuery, ICollection<EnrollmentDto>>
    {
        public Task<ICollection<EnrollmentDto>> Handle(GetEnrollmentByIdQuery request, CancellationToken cancellationToken)
        {
            return repository.GetAllEnrollmentByID(request.id);
        }
    }
}
