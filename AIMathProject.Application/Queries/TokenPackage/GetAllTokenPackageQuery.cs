using AIMathProject.Application.Dto.Payment.TokenPackageDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.TokenPackage
{
    public record GetAllTokenPackageQuery() : IRequest<ICollection<TokenPackageDto>>;
    public class GetAllTokenPackageQueryHandler(ITokenPackageRepository<TokenPackageDto> repository) :
        IRequestHandler<GetAllTokenPackageQuery, ICollection<TokenPackageDto>>
    {
        public Task<ICollection<TokenPackageDto>> Handle(GetAllTokenPackageQuery request, CancellationToken cancellationToken)
        {
            return repository.GetAllInfoTokenPackage();
        }
    }
}
