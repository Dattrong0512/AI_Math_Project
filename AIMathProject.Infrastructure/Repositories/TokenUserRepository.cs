using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class TokenUserRepository : ITokenUserRepository<TokenUser>
    {
        private readonly ApplicationDbContext _context;

        public TokenUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddTokenUser(TokenUser tokenUs)
        {
            await _context.TokenUsers.AddAsync(tokenUs);
            int row = await _context.SaveChangesAsync();
            if(row>0)
            {
                TokenTransaction TkTransaction = new TokenTransaction
                {
                    TokenUserId = tokenUs.UserId,
                    Amount = tokenUs.Tokens,
                    Date = DateTime.Now
                };
                await _context.TokenTransactions.AddAsync(TkTransaction);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
