using AIMathProject.Application.Dto.Payment.MethodDto;
using AIMathProject.Application.Dto.Payment.PaymentDto;
using AIMathProject.Application.Dto.Payment.PlanDto;
using AIMathProject.Application.Mappers.PaymentServices;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
                    UserId = pm.Wallet.UserId,
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
                        MethodName = pmt.MethodName,
                        MethodIcon = pmt.MethodIcon
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
                    UserId = pm.Wallet.UserId,
                    TransactionID = pm.TransactionId,
                    PlanId = pm.PlanId,
                    Date = pm.Date,
                    Description = pm.Description,
                    Status = pm.Status,
                    Price = pm.Price,
                    Method = new MethodDto
                    {
                        MethodId = pmt.MethodId,
                        MethodName = pmt.MethodName,
                        MethodIcon = pmt.MethodIcon
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

        public async Task<(List<PaymentDto> items, int totalCount, int pageIndex, int pageSize)> GetAllPaymentsPaginated(int pageIndex, int pageSize)
        {
            try
            {

                // Get total count for pagination
                var totalCount = await _context.Payments.CountAsync();

                // Build query with joins
                var query = from pm in _context.Payments
                            join pmt in _context.PaymentMethods
                            on pm.MethodId equals pmt.MethodId
                            join pl in _context.Plans
                            on pm.PlanId equals pl.PlanId into planGroup
                            from pl in planGroup.DefaultIfEmpty()
                            select new PaymentDto
                            {
                                PaymentId = pm.PaymentId,
                                UserId = pm.Wallet.UserId,
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
                                    MethodName = pmt.MethodName,
                                    MethodIcon = pmt.MethodIcon
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

                // Order by date descending and apply pagination
                var items = await query
                    .OrderByDescending(p => p.Date)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return (items, totalCount, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(List<PaymentDto> items, int totalCount, int pageIndex, int pageSize)> GetAllPaymentsByUserPaginated(int userId, int pageIndex, int pageSize)
        {
            try
            {
                int walletId = _context.Wallets
                    .Where(w => w.UserId == userId)
                    .Select(w => w.WalletId)
                    .FirstOrDefault();

                var totalCount = await _context.Payments
                    .Where(p => p.WalletId == walletId)
                    .CountAsync();

                var query = from pm in _context.Payments
                            join pmt in _context.PaymentMethods
                            on pm.MethodId equals pmt.MethodId
                            join pl in _context.Plans
                            on pm.PlanId equals pl.PlanId into planGroup
                            from pl in planGroup.DefaultIfEmpty()
                            where pm.WalletId == walletId
                            select new PaymentDto
                            {
                                PaymentId = pm.PaymentId,
                                MethodId = pm.MethodId,
                                WalletId = pm.WalletId,
                                UserId = pm.Wallet.UserId,
                                TransactionID = pm.TransactionId,
                                PlanId = pm.PlanId,
                                Date = pm.Date,
                                Description = pm.Description,
                                Status = pm.Status,
                                Price = pm.Price,
                                Method = new MethodDto
                                {
                                    MethodId = pmt.MethodId,
                                    MethodName = pmt.MethodName,
                                    MethodIcon = pmt.MethodIcon
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

                var items = await query
                    .OrderByDescending(p => p.Date)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return (items, totalCount, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paginated payments for user {UserId}", userId);
                throw;
            }
        }
    }
}
 