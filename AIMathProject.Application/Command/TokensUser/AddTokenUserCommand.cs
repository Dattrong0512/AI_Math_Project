using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.TokensUser
{
    public record AddTokenUserCommand(TokenUser tokenUser) : IRequest<bool>;
    public class AddTokenUserCommandHandler(ITokenUserRepository<TokenUser> repository) :
        IRequestHandler<AddTokenUserCommand, bool>
    {
        public async Task<bool> Handle(AddTokenUserCommand request, CancellationToken cancellationToken)
        {
            return await repository.AddTokenUser(request.tokenUser);
        }
    }
}
