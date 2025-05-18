using AIMathProject.Application.Dto.Payment.MethodDto;
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
    public class PaymentMethodRepository : IPaymentMethodRepository<MethodDto>
    {
        private readonly ApplicationDbContext _context;

        public PaymentMethodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<MethodDto>> GetAllPaymentMethod()
        {
            List<PaymentMethod> method = await _context.PaymentMethods.ToListAsync();

            return method.ToListPaymentMethodDto();
        }
    }
}
