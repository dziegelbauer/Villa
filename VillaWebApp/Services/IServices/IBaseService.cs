using VillaWebApp.Models;

namespace VillaWebApp.Services.IServices;

public interface IBaseService
{
    APIResponse ResponseModel { get; set; }

    Task<T> SendAsync<T>(APIRequest apiRequest);
}