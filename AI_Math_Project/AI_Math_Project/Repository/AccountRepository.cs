using AI_Math_Project.Data;
using AI_Math_Project.Data.Model;
using AI_Math_Project.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDBContext _context;

        public AccountRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Account> Login(string email, string password)
        {
            var account = await _context.Accounts.Where(ac => ac.Email == email && ac.Password == password).FirstOrDefaultAsync();
            if (account != null )
            {
                Account ac = account;
                return ac;
            }
            else
            {
             
                throw new Exception("Not exist account");
            }
        }
        public async Task<Account> GetInfo(int id)
        {
            var account = await _context.Accounts.Where(ac => ac.AccountId == id ).FirstOrDefaultAsync();
            if (account != null)
            {
                Account ac = account;
                return ac;
            }
            else
            {

                throw new Exception("Not exist account");
            }
        }
    }
}
