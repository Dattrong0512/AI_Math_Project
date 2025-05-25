using AIMathProject.Application.Abstracts;
using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Application.Dto.UserDto;
using AIMathProject.Domain.Exceptions;
using MediatR;

namespace AIMathProject.Application.Queries.Users
{
    public record GetUsersWithFiltersQuery(
        string? SearchTerm,
        int? Role,
        bool? Status,
        int PageIndex,
        int PageSize) : IRequest<Pagination<UserDto>>;

    public class GetUsersWithFiltersHandler : IRequestHandler<GetUsersWithFiltersQuery, Pagination<UserDto>>
    {
        private readonly IUserRepository _repository;

        public GetUsersWithFiltersHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Pagination<UserDto>> Handle(GetUsersWithFiltersQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetUsersWithFilters(
                request.SearchTerm,
                request.Role,
                request.Status,
                request.PageIndex,
                request.PageSize);

            if (result == null)
            {
                throw new NoDataFoundException("User");
            }
            return result;
        }
    }
}