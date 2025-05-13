using AIMathProject.Application.Dto.Payment.PaymentDto;
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
    public class PaymentRepository : IPaymentRepository<PaymentDto>
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPayment(PaymentDto dto)
        {
            Payment payment = dto.ToPayment();
            await _context.AddAsync(payment);

            int rows = await _context.SaveChangesAsync(); // trả về số bản ghi đã ảnh hưởng

            if (rows > 0)
            {
                return true;
            }
            return false;
        }


        public async Task<PaymentDto> GetInfoPaymentUser(int id)
        {
            Payment payment = await _context.Payments.Where(us => us.UserId == id)
                                    .FirstOrDefaultAsync();

            if (payment != null)
            {
                return payment.ToPaymentDto();
            }
            else return null;
        }

    }
}
