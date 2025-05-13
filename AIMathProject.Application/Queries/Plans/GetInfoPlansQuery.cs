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
    public record GetInfoPlansQuery(int id) : IRequest<PlansDto>;
    public class GetInfoPlansQueryHandler(IPlanRepository<PlansDto> repository) :
        IRequestHandler<GetInfoPlansQuery, PlansDto>
    {
        public Task<PlansDto> Handle(GetInfoPlansQuery request, CancellationToken cancellationToken)
        {
            return repository.GetInfoPlanByID(request.id);
        }
    }
}
