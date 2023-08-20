using DataAccess.Context;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Configurations;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {

        private AppDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public UserService(AppDbContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;

        }

        public async Task<(bool Success, string ErrorMessage, User User)> CreateNewUserAsync(RegisterUserModel model)
        {
            if(_context.User.Any(u => u.Username == model.Username)) 
            {
                return (false, "Username already exists.", null);
            }

            CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var newUser = new User { Username = model.Username, PasswordHash = passwordHash, PasswordSalt = passwordSalt };

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            return (true, null, newUser);

        }

        public async Task<User> Authenticate(string username, string password)
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _context.User.SingleOrDefaultAsync(x => x.Username == username);

            // Check if username exists
            if (user == null)
                return null;

            // Check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // Authentication successful
            return user;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt) 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
