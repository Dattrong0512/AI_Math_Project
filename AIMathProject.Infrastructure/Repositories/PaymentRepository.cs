using AIMathProject.Application.Dto.Payment.MethodDto;
using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Application.Dto.Payment.PlanDto;
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

        public Task<List<PaymentDto>> GetAllPayment(int userID)
        {
            int wlId = _context.Wallets
                .Where(w => w.UserId == userID)
                .Select(w => w.WalletId)
                .FirstOrDefault();
            var listPayment =
                from pm in _context.Payments
                join pmt in _context.PaymentMethods
                on pm.MethodId equals pmt.MethodId
                join pl in _context.Plans
                on pm.PlanId equals pl.PlanId into planGroup
                from pl in planGroup.DefaultIfEmpty()
                where pm.WalletId == wlId
                select new PaymentDto
                {
                    PaymentId = pm.PaymentId,
                    MethodId = pm.MethodId,
                    WalletId = pm.WalletId,
                    TransactionID = pm.TransactionId,
                    PlanId = pm.PlanId,
                    Date = pm.Date,
                    Description = pm.Description,
                    Status = pm.Status,
                    Price = pm.Price,
                    Method = new MethodDto
                    {
                        MethodId = pmt.MethodId,
                        MethodName = pmt.MethodName
                    },
                    Plan = pl != null ? new PlansDto
                    {
                        PlanId = pl.PlanId,
                        PlanName = pl.PlanName,
                        Price = pl.Price,
                        Coins = pl.Coins,
                        Description = pl.Description,
                    } : null
                };
            return listPayment.ToListAsync();
        }

        public Task<PaymentDto> GetLatestPayment(int userID)
        {
            int wlId = _context.Wallets
              .Where(w => w.UserId == userID)
              .Select(w => w.WalletId)
              .FirstOrDefault();
            var listPayment =
                from pm in _context.Payments
                join pmt in _context.PaymentMethods
                on pm.MethodId equals pmt.MethodId
                join pl in _context.Plans
                on pm.PlanId equals pl.PlanId into planGroup
                from pl in planGroup.DefaultIfEmpty()
                where pm.WalletId == wlId
                orderby pm.Date descending
                select new PaymentDto
                {
                    PaymentId = pm.PaymentId,
                    MethodId = pm.MethodId,
                    WalletId = pm.WalletId,
                    TransactionID = pm.TransactionId,
                    PlanId = pm.PlanId,
                    Date = pm.Date,
                    Description = pm.Description,
                    Status = pm.Status,
                    Price = pm.Price,
                    Method = new MethodDto
                    {
                        MethodId = pmt.MethodId,
                        MethodName = pmt.MethodName
                    },
                    Plan = pl != null ? new PlansDto
                    {
                        PlanId = pl.PlanId,
                        PlanName = pl.PlanName,
                        Price = pl.Price,
                        Coins = pl.Coins,
                        Description = pl.Description,
                    } : null
                };
            return listPayment.FirstOrDefaultAsync();

        }

        public async Task<List<PaymentDto>> GetAllPaymentsFilterByDate(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var query = from pm in _context.Payments
                            join pmt in _context.PaymentMethods
                            on pm.MethodId equals pmt.MethodId
                            join pl in _context.Plans
                            on pm.PlanId equals pl.PlanId into planGroup
                            from pl in planGroup.DefaultIfEmpty()
                            select new PaymentDto
                            {
                                PaymentId = pm.PaymentId,
                                MethodId = pm.MethodId,
                                WalletId = pm.WalletId,
                                TransactionID = pm.TransactionId,
                                PlanId = pm.PlanId,
                                Date = pm.Date,
                                Description = pm.Description,
                                Status = pm.Status,
                                Price = pm.Price,
                                Method = new MethodDto
                                {
                                    MethodId = pmt.MethodId,
                                    MethodName = pmt.MethodName
                                },
                                Plan = pl != null ? new PlansDto
                                {
                                    PlanId = pl.PlanId,
                                    PlanName = pl.PlanName,
                                    Price = pl.Price,
                                    Coins = pl.Coins,
                                    Description = pl.Description,
                                } : null
                            };

                if (startDate.HasValue)
                {
                    var startOfDay = startDate.Value.Date;
                    query = query.Where(p => p.Date >= startOfDay);
                }

                if (endDate.HasValue)
                {
                    var endOfDay = endDate.Value.Date.AddDays(1).AddMilliseconds(-1);
                    query = query.Where(p => p.Date <= endOfDay);
                }

                query = query.OrderByDescending(p => p.Date);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
 