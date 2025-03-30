using AIMathProject.Application.Abstracts;
using AIMathProject.Application.Dto.UserDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Users
{
    public record GetInfoUserLoginQuery : IRequest<UserDto>;
    public class GetInfoUserLoginHandler(IUserRepository repository) : IRequestHandler<GetInfoUserLoginQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetInfoUserLoginQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetInfoUserLogin();
        }
    }
}
