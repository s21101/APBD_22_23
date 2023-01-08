using Cw9.Models;
using Cw9.DTO.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Cw9.DTO.Responce;

namespace Cw9.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly PharmacyContext _context;
        string secret = "qazwsxedc23ndiWr5^$S%^^YTDREWXSDFGHUYTREW@##$%^&*NBGTR%DXCVGHYGVBHYTRDXCV";
        public AccountsService(PharmacyContext pharmacyContext)
        {
            _context = pharmacyContext;
        }

        public async Task<bool> RegisterUser(RegisterDTO newUser)
        {
            if (await IsUserExists(newUser.Login))
            { 
                return false;
            }

            
            var salt = PasswordEncrypter.CreateSalt();
            var pass = PasswordEncrypter.Encrypt(newUser.Password, salt);

            if (!PasswordEncrypter.ValidatePassworEncryption(newUser.Password, salt, pass)) 
            {
                throw new Exception("problem while pass encrypting");
            }
            

            //var hasher = new PasswordHasher<RegisterDTO>();
            //var hashedPassword = hasher.HashPassword(newUser, newUser.Password);
            

            User u = new User
            {
                Login = newUser.Login,
                Password = pass,
                Salt = salt
            };

            await _context.Users.AddAsync(u);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<TokenDTO> Login(LoginDTO loginRequest)
        { 
            User u = await _context
                          .Users
                          .SingleOrDefaultAsync(e => e.Login == loginRequest.Login);

            bool isPasswordValid = PasswordEncrypter.ValidatePassworEncryption(loginRequest.Password, u.Salt, u.Password);

            if (u == null || !isPasswordValid)
            {
                return null;
            }

            var claims = new Claim[]
        {
            new(ClaimTypes.Name, u.Login),
            new("Custom", "SomeData"),
            new Claim(ClaimTypes.Role, "admin")
        };

            var secret = "qazwsxedc23ndiWr5^$S%^^YTDREWXSDFGHUYTREW@##$%^&*NBGTR%DXCVGHYGVBHYTRDXCV";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var options = new JwtSecurityToken("https://localhost:7157", "https://localhost:7157",
                claims, expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: creds);

            var refreshToken = "";
            using (var genNum = RandomNumberGenerator.Create())
            {
                var r = new byte[1024];
                genNum.GetBytes(r);
                refreshToken = Convert.ToBase64String(r);
            }

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(options),
                RefreshToken = refreshToken
            };
        }

        private async Task<bool> IsUserExists(string login)
        {
            User u = await _context.Users.Where(e => e.Login == login).SingleOrDefaultAsync();
            if (u == null)
            {
                return false;
            }

            return true;
        }
    }
}
