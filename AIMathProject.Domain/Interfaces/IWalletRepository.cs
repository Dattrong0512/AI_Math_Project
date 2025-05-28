using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IWalletRepository<T> where T : class
    {
        public Task<bool> UpdateCoins(int UserId, int amount);
        public Task<string> UpdateCoinBuyToken(int userID, int TokenPackageID);
        public Task<bool> UpdateTokenUsed(int WalletId, int amount);

        public Task<T> GetWalletByUserId(int userId);
    }
}
