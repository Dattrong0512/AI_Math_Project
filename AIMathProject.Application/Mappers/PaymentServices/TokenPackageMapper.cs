using AIMathProject.Application.Dto.Payment.PlanDto;
using AIMathProject.Application.Dto.Payment.TokenPackageDto;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Mappers.PaymentServices
{
    public static class TokenPackageMapper 
    {
        public static TokenPackageDto ToTokenPackageDto (this TokenPackage package)
        {
            TokenPackageDto dto = new TokenPackageDto
            {
                TokenPackageId = package.TokenPackageId,

                 PackageName = package.PackageName,

                Tokens = package.Tokens,

                Price = package.Price,

                Description = package.Description
            };
            return dto;

        }
        public static ICollection<TokenPackageDto> ToListTokenPackageDto(this ICollection<TokenPackage> list)
        {
            List<TokenPackageDto> dto = new List<TokenPackageDto>();
            foreach (var pl in list)
            {
                dto.Add(pl.ToTokenPackageDto());
            }
            return dto;
        }
    }
}
