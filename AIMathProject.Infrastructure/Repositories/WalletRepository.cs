using AIMathProject.Application.Dto.Payment.CoinTransaction;
using AIMathProject.Application.Dto.Payment.TokenPackage;
using AIMathProject.Application.Dto.Payment.Wallet;
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
    public class WalletRepository : IWalletRepository<WalletDto>
    {
        public readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WalletDto> UpdateCoinUsed(int walletId, int amount)
        {
            Wallet wallet = new Wallet();
            wallet = _context.Wallets.FirstOrDefault(w => w.WalletId == walletId);
            if (wallet == null)
            {
                return null;
            }

            wallet.CoinRemains -= amount;

            CoinTransaction coinTransaction = new CoinTransaction
            {
                WalletId = wallet.WalletId,
                CoinRemains = wallet.CoinRemains,
                Amount = -amount,
                Date = DateTime.UtcNow,
                IsTokenPackage = false,
                TokenPackageId = null,
                TokenPackage = null
            };
            _context.CoinTransactions.Add(coinTransaction);
            await _context.SaveChangesAsync();
            return new WalletDto
            {
                WalletId = wallet.WalletId,
                UserId = wallet.UserId,
                CoinRemains = wallet.CoinRemains,
                TokenRemains = wallet.TokenRemains
            }
            ;
        }

        public async Task<string> UpdateCoinBuyToken(int userID, int TokenPackageID)
        {
            var wallet = _context.Wallets.FirstOrDefault(w => w.UserId == userID);
            if (wallet == null)
            {
                throw new Exception("Wallet not found for user ID: " + userID);
            }
            TokenPackage tkpk = new TokenPackage();
            tkpk = _context.TokenPackages.FirstOrDefault(t => t.TokenPackageId == TokenPackageID);
            if (tkpk == null)
            {
                throw new Exception("Token package not found for ID: " + TokenPackageID);
            }
            int amoutCoin = tkpk.Price ?? 0;
            int amountToken = tkpk.Tokens ?? 0;

            if (wallet.CoinRemains - amoutCoin < 0)
            {
                throw new Exception("Insufficient coins in wallet for user ID: " + userID);
            }
            wallet.CoinRemains -= amoutCoin;
            wallet.TokenRemains += amountToken;


            CoinTransaction coinTransaction = new CoinTransaction
            {
                WalletId = wallet.WalletId,
                CoinRemains = wallet.CoinRemains,
                Amount = -amoutCoin,
                Date = DateTime.UtcNow,
                IsTokenPackage = true,
                TokenPackageId = TokenPackageID,
                TokenPackage = tkpk
            };
            _context.CoinTransactions.Add(coinTransaction);
            await _context.SaveChangesAsync();
            return "Thanh toán thành công cho gói token " + tkpk.PackageName + " với số lượng " + tkpk.Tokens + " token.";
        }

        public async Task<WalletDto> UpdateTokenUsed(int walletId, int TokenRemain)
        {
            Wallet wallet = _context.Wallets.FirstOrDefault(w => w.WalletId == walletId);
            if (wallet == null)
            {
                return null;
            }
            wallet.TokenRemains -= TokenRemain;
            var tokenTransaction = new Domain.Entities.TokenTransaction
            {
                WalletId = wallet.WalletId,
                TokenRemains = wallet.TokenRemains,
                TokenAmount = -TokenRemain,
                Date = DateTime.UtcNow
            };
            _context.TokenTransactions.Add(tokenTransaction);
            await _context.SaveChangesAsync();
            return new WalletDto
            {
                WalletId = wallet.WalletId,
                UserId = wallet.UserId,
                CoinRemains = wallet.CoinRemains,
                TokenRemains = wallet.TokenRemains
            }
            ;
        }

        public async Task<WalletDto> GetWalletByUserId(int userId)
        {
            Wallet wallet = await _context.Wallets.Where(wl => wl.UserId == userId).FirstOrDefaultAsync();
            if (wallet == null)
            {
                return null;
            }
            else
            {
                WalletDto dto = new WalletDto
                {
                    WalletId = wallet.WalletId,
                    UserId = wallet.UserId,
                    CoinRemains = wallet.CoinRemains,
                    TokenRemains = wallet.TokenRemains,
                };
                return dto;
            }
        }
    }
}
