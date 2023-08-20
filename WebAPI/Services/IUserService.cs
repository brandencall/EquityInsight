using DataAccess.Models;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IUserService
    {
        Task<(bool Success, string ErrorMessage, User User)> CreateNewUserAsync(RegisterUserModel model);
        Task<User> Authenticate(string username, string password);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
        string GenerateJwtToken(User user);
    }
}
