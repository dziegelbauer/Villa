using Villa_Utility;
using VillaWebApp.Models;
using VillaWebApp.Models.DTO;
using VillaWebApp.Services.IServices;

namespace VillaWebApp.Services;

public class VillaNumberService : BaseService, IVillaNumberService
{
    private readonly string _apiUrl;
    private readonly string _apiPath = "/api/VillaNumberAPI";

    public VillaNumberService(IHttpClientFactory httpClientFactory, IConfiguration configuration) 
        : base(httpClientFactory)
    {
        _apiUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI")!;
    }
    
    public async Task<T> GetAsync<T>(int id)
    {
        return await SendAsync<T>(new APIRequest()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = $"{_apiUrl}{_apiPath}/{id}",
            Data = null,
        });
    }

    public async Task<T> GetAllAsync<T>()
    {
        return await SendAsync<T>(new APIRequest()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = $"{_apiUrl}{_apiPath}",
            Data = null,
        });
    }

    public async Task<T> CreateAsync<T>(VillaNumberCreateDTO dto)
    {
        return await SendAsync<T>(new APIRequest()
        {
            ApiType = StaticDetails.ApiType.POST,
            Url = $"{_apiUrl}{_apiPath}",
            Data = dto
        });
    }

    public async Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dto)
    {
        return await SendAsync<T>(new APIRequest()
        {
            ApiType = StaticDetails.ApiType.PUT,
            Url = $"{_apiUrl}{_apiPath}/{dto.VillaNo}",
            Data = dto
        });
    }

    public async Task<T> DeleteAsync<T>(int id)
    {
        return await SendAsync<T>(new APIRequest()
        {
            ApiType = StaticDetails.ApiType.DELETE,
            Url = $"{_apiUrl}{_apiPath}/{id}",
            Data = null,
        });
    }
}