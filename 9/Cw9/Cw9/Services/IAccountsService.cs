using Cw9.DTO.Request;
using Cw9.DTO.Responce;

namespace Cw9.Services
{
    public interface IAccountsService
    {
        Task<bool> RegisterUser(RegisterDTO newUser);
        Task<TokenDTO> Login(LoginDTO login);
    }
}
