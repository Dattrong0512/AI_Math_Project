using System.Runtime.CompilerServices;

namespace AI_Math_Project.Interfaces
{
    public interface IRefreshTokenRepository
    {
        public string GenerateRefreshToken();
        Task<string> SaveRefreshToken(int account_id);

        Task<string> CheckRefreshToken(int accound_id);
    }
}
