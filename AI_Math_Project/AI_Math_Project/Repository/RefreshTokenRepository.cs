using AI_Math_Project.Interfaces;
using AI_Math_Project.DTO.LoginDto;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using AI_Math_Project.Data;
using AI_Math_Project.Data.Model;



namespace AI_Math_Project.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {

        private readonly ApplicationDBContext _context;

        public RefreshTokenRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            RandomNumberGenerator.Fill(randomNumber);  // Sử dụng RandomNumberGenerator để tạo số ngẫu nhiên
            return Convert.ToBase64String(randomNumber);  // Chuyển đổi byte array thành Base64 string
        }

        public async Task<string> SaveRefreshToken(int account_id)
        {
            RefreshToken token = new RefreshToken  
            {
                AccountId = account_id,
                Token = GenerateRefreshToken(),
                ExpiryDate = DateTime.Now.AddDays(90)
            };
            await _context.AddAsync(token);
            await _context.SaveChangesAsync();
            return token.Token;
        }

        public async Task<string> CheckRefreshToken(int accound_id)
        {
            var token = await _context.RefreshTokens.Where(rt => rt.AccountId == accound_id && rt.ExpiryDate > DateTime.Now)
                .FirstOrDefaultAsync();

            if(token is null)
            {
                return "RefreshToken is not exists";
            }
            else
            {
                return token.Token;
            }
                   
        }

   
    }
}
