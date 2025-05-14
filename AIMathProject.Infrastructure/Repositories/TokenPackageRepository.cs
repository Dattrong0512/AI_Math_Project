using AIMathProject.Application.Dto.Payment.TokenPackageDto;
using AIMathProject.Application.Mappers.PaymentServices;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class TokenPackageRepository : ITokenPackageRepository<TokenPackageDto>
    {
        private readonly ApplicationDbContext _context;

        public TokenPackageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TokenPackageDto> GetInfoTokenPackageById(int id)
        {
            TokenPackage package = await _context.TokenPackages.FirstOrDefaultAsync(tkp => tkp.TokenPackageId == id);
            return package.ToTokenPackageDto();
        }
    }
}
