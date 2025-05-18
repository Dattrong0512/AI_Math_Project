using AIMathProject.Application.Dto.Payment.MethodDto;
using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Application.Mappers.PaymentServices;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository<PaymentDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PaymentRepository> _logger;


        public PaymentRepository(ApplicationDbContext context, ILogger<PaymentRepository> logger)
        {
            _context = context;
            _logger = logger;
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

        public async Task<ICollection<PaymentDto>> GetAllInfoPaymentUserById(int id)
        {
            var paymentDtos = await (from pm in _context.Payments
                                     join pmt in _context.PaymentMethods on pm.MethodId equals pmt.MethodId
                                     join pl in _context.Plans on pm.PlanId equals pl.PlanId into plJoin
                                     from pl in plJoin.DefaultIfEmpty() // LEFT JOIN cho Plans
                                     join tkp in _context.TokenPackages on pm.TokenPackageId equals tkp.TokenPackageId into tkpJoin
                                     from tkp in tkpJoin.DefaultIfEmpty() // LEFT JOIN cho TokenPackage
                                     where pm.UserId == id
                                     select new PaymentDto
                                     {
                                         PaymentId = pm.PaymentId,
                                         MethodId = pm.MethodId,
                                         UserId = pm.UserId,
                                         TokenPackageId = pm.TokenPackageId,
                                         PlanId = pm.PlanId,
                                         Date = pm.Date,
                                         Description = pm.Description,
                                         Status = pm.Status,
                                         Price = pm.Price,
                                         OrderID = pm.OrderId,
                                         TransactionID = pm.TransactionId,
                                         Method = pmt != null ? pmt.ToMethodDto() : null,
                                         Plan = pl != null ? pl.ToPlansDto() : null,
                                         TokenPackage = tkp != null ? tkp.ToTokenPackageDto() : null
                                     })
                                     .ToListAsync();

            _logger.LogInformation($"logger: Retrieved {paymentDtos.Count} payment records for user {id}");

            if (!paymentDtos.Any())
            {
                throw new KeyNotFoundException($"Không tìm thấy thanh toán nào cho user_id {id}.");
            }

            return paymentDtos;
        }

        public async Task<PaymentDto> GetLatestInfoPaymentUserById(int id)
        {
            PaymentDto ?paymentDto = await (from pm in _context.Payments
                                           join pmt in _context.PaymentMethods on pm.MethodId equals pmt.MethodId
                                           join pl in _context.Plans on pm.PlanId equals pl.PlanId into plJoin
                                           from pl in plJoin.DefaultIfEmpty() // LEFT JOIN cho Plans
                                           join tkp in _context.TokenPackages on pm.TokenPackageId equals tkp.TokenPackageId into tkpJoin
                                           from tkp in tkpJoin.DefaultIfEmpty() // LEFT JOIN cho TokenPackage
                                           where pm.UserId == id
                                           orderby pm.PaymentId descending
                                           select new PaymentDto
                                           {
                                               PaymentId = pm.PaymentId,
                                               MethodId = pm.MethodId,
                                               UserId = pm.UserId,
                                               TokenPackageId = pm.TokenPackageId,
                                               PlanId = pm.PlanId,
                                               Date = pm.Date,
                                               Description = pm.Description,
                                               Status = pm.Status,
                                               Price = pm.Price,
                                               OrderID = pm.OrderId,
                                               TransactionID = pm.TransactionId,
                                               Method = pmt != null ? pmt.ToMethodDto() : null,
                                               Plan = pl != null ? pl.ToPlansDto() : null,
                                               TokenPackage = tkp != null ? tkp.ToTokenPackageDto() : null
                                           })
                              .FirstOrDefaultAsync();
            _logger.LogInformation($"logger {paymentDto}");
            if (paymentDto != null)
            {
                return paymentDto;
            }
            else return null;
        }

    }
}
 