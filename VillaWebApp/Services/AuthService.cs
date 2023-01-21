using Villa_Utility;
using VillaWebApp.Models;
using VillaWebApp.Models.DTO;
using VillaWebApp.Services.IServices;

namespace VillaWebApp.Services;

public class AuthService : BaseService, IAuthService
{
    private readonly string _apiUrl;
    private readonly string _apiPath = "/api/UsersAuth";

    public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
    {
        _apiUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI")!;
    }

    public async Task<T> LoginAsync<T>(LoginRequestDTO obj)
    {
        return await SendAsync<T>(new APIRequest()
        {
            ApiType = StaticDetails.ApiType.POST,
            Data = obj,
            Url = $"{_apiUrl}{_apiPath}/login",
        });
    }

    public async Task<T> RegisterAsync<T>(RegistrationRequestDTO obj)
    {
        return await SendAsync<T>(new APIRequest()
        {
            ApiType = StaticDetails.ApiType.POST,
            Data = obj,
            Url = $"{_apiUrl}{_apiPath}/register",
        });
    }
}