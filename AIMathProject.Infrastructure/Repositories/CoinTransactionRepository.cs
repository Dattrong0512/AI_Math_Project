//using AIMathProject.Application.Dto.Payment.CoinTransaction;
//using AIMathProject.Application.Mappers.PaymentServices;
//using AIMathProject.Domain.Entities;
//using AIMathProject.Domain.Interfaces;
//using AIMathProject.Infrastructure.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AIMathProject.Infrastructure.Repositories
//{
//    public class CoinTransactionRepository : ICoinTransactionRepository<CoinTransactionDto>
//    {
//        private readonly ApplicationDbContext _context;

//        public CoinTransactionRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<bool> AddCoinTransaction(CoinTransactionDto dto)
//        {
//            CoinTransaction cointransaction = CoinTransactionMapper.ToCoinTransaction(dto);
//            await _context.AddAsync(cointransaction);
//            await _context.SaveChangesAsync();
//            return true;
//        }
//    }
//}
