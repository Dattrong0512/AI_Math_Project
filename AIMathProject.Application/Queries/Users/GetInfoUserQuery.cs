using AIMathProject.Application.Abstracts;
using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Application.Dto.UserDto;
using AIMathProject.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Users
{
    public record GetInfoUserQuery(int pageIndex, int  pageSize) : IRequest<Pagination<UserDto>>;
    public class GetInfoUserHandler(IUserRepository _repository) : IRequestHandler<GetInfoUserQuery, Pagination<UserDto>>
    {
        public async Task<Pagination<UserDto>> Handle(GetInfoUserQuery request, CancellationToken cancellationToken)
        {
            var infoReturn = await _repository.GetInfoUser(request.pageIndex, request.pageSize);
            if (infoReturn == null)
            {
                throw new NoDataFoundException("User");
            }
            else return infoReturn;
        }
    }
}
