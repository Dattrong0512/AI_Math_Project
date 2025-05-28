using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IWalletRepository<T> where T : class
    {
        public Task<T> UpdateCoinUsed(int walletId, int amount);
        public Task<string> UpdateCoinBuyToken(int userID, int TokenPackageID);
        public Task<T> UpdateTokenUsed(int walletId, int TokenRemain);

        public Task<T> GetWalletByUserId(int userId);
    }
}
