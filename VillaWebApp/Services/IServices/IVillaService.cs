using VillaWebApp.Models.DTO;

namespace VillaWebApp.Services.IServices;

public interface IVillaService
{
    Task<T> GetAsync<T>(int id);
    Task<T> GetAllAsync<T>();
    Task<T> CreateAsync<T>(VillaCreateDTO dto);
    Task<T> UpdateAsync<T>(VillaUpdateDTO dto);
    Task<T> DeleteAsync<T>(int id);
}