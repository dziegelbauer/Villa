using VillaWebApp.Models.DTO;

namespace VillaWebApp.Services.IServices;

public interface IVillaNumberService
{
    Task<T> GetAsync<T>(int id);
    Task<T> GetAllAsync<T>();
    Task<T> CreateAsync<T>(VillaNumberCreateDTO dto);
    Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dto);
    Task<T> DeleteAsync<T>(int id);
}