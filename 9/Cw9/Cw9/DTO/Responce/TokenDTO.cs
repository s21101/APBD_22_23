using System.IdentityModel.Tokens.Jwt;

namespace Cw9.DTO.Responce
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
