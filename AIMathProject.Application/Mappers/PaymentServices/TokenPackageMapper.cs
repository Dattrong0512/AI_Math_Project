using AIMathProject.Application.Dto.Payment.PlanDto;
using AIMathProject.Application.Dto.Payment.TokenPackage;
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
        public static TokenPackageDto ToTokenPackageDto(this TokenPackage package)
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
        public static List<TokenPackageDto> ToListTokenPackageDto(ICollection<TokenPackage> list)
        {
            List<TokenPackageDto> dto = new List<TokenPackageDto>();
            foreach (var item in list)
            {
                dto.Add(item.ToTokenPackageDto());
            }
            return dto;
        }
        public static TokenPackage ToTokenPackage(this TokenPackageDto dto)
        {
            TokenPackage package = new TokenPackage
            {
                TokenPackageId = dto.TokenPackageId,
                PackageName = dto.PackageName,
                Tokens = dto.Tokens,
                Price = dto.Price,
                Description = dto.Description
            };
            return package;
        }
    }
}
