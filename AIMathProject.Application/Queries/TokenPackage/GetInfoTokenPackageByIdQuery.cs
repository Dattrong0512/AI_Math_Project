using AIMathProject.Application.Dto.Payment.TokenPackage;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.TokenPackage
{
    public record GetInfoTokenPackageByIdQuery(int id) : IRequest<TokenPackageDto>;
    public class GetInfoTokenPackageByIdQueryHandler(ITokenPackageRepository<TokenPackageDto> repository)
        : IRequestHandler<GetInfoTokenPackageByIdQuery, TokenPackageDto>
    {
        public Task<TokenPackageDto> Handle(GetInfoTokenPackageByIdQuery request, CancellationToken cancellationToken)
        {
            return repository.GetInfoTokenPackageById(request.id);
        }
    }
}
