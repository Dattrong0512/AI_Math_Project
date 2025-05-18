using AIMathProject.Application.Dto.Payment.PlanDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Plans
{
    public record GetAllPlansQuery() : IRequest<ICollection<PlansDto>>;
    public class GetAllPlansQueryHandler(IPlanRepository<PlansDto> repository) :
        IRequestHandler<GetAllPlansQuery, ICollection<PlansDto>>
    {
        public Task<ICollection<PlansDto>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
        {
            return repository.GetAllPlan();
        }
    }
}
