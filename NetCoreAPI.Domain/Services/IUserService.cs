using NetCoreAPI.Domain.Models;

namespace NetCoreAPI.Domain.Services
{
    public interface IUserService
    {
        AuthenticateResponse Register(RegisterRequest model, string ipAddress);
        AuthenticateResponse Login(LoginRequest model, string ipAddress);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
    }
}
