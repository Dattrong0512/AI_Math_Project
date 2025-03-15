using AI_Math_Project.Data.Model;

namespace AI_Math_Project.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> Login(string email, string password);

        Task<Account> GetInfo(int id);
    }
}
