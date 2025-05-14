using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.PlansUser
{
    public record AddPlanUserCommand(PlanUser plUser) : IRequest<bool>;
    public class AddPlanUserCommandHandler(IPlanUserRepository<PlanUser> repository) : IRequestHandler<AddPlanUserCommand, bool>
    {
        public Task<bool> Handle(AddPlanUserCommand request, CancellationToken cancellationToken)
        {
            return repository.AddPlanUser(request.plUser);
        }
    }
}
