using VillaWebApp.Models.DTO;

namespace VillaWebApp.Services.IServices;

public interface IAuthService
{
    Task<T> LoginAsync<T>(LoginRequestDTO obj);
    Task<T> RegisterAsync<T>(RegistrationRequestDTO obj);
}