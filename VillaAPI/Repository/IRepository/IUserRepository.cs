using VillaAPI.Models;
using VillaAPI.Models.DTO;

namespace VillaAPI.Repository.IRepository;

public interface IUserRepository : IRepository<LocalUser>
{
    bool IsUniqueUser(string username);
    Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO);
    Task<LocalUser> RegisterAsync(RegistrationRequestDTO registrationRequestDTO);
    Task<LocalUser> UpdateAsync(LocalUser entity);
}