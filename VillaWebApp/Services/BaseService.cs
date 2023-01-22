using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Villa_Utility;
using VillaWebApp.Models;
using VillaWebApp.Services.IServices;

namespace VillaWebApp.Services;

public class BaseService : IBaseService
{
    public APIResponse ResponseModel { get; set; }
    private readonly IHttpClientFactory _httpClientFactory;

    public BaseService(IHttpClientFactory httpClientFactory)
    {
        ResponseModel = new();
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<T> SendAsync<T>(APIRequest apiRequest)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("VillaAPI");
            var message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(apiRequest.Url);

            if (apiRequest.Data is not null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), 
                    Encoding.UTF8, "application/json");
            }

            message.Method = apiRequest.ApiType switch
            {
                StaticDetails.ApiType.GET => HttpMethod.Get,
                StaticDetails.ApiType.POST => HttpMethod.Post,
                StaticDetails.ApiType.PUT => HttpMethod.Put,
                StaticDetails.ApiType.DELETE => HttpMethod.Delete,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (!string.IsNullOrEmpty(apiRequest.Token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
            }

            HttpResponseMessage? httpResponse = await client.SendAsync(message);

            var apiContent = await httpResponse.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);

            if (apiResponse is not null
                && (apiResponse.StatusCode == HttpStatusCode.BadRequest
                    || apiResponse.StatusCode == HttpStatusCode.NotFound))
            {
                apiResponse.StatusCode = HttpStatusCode.BadRequest;
                apiResponse.IsSuccessful = false;
                var res = JsonConvert.SerializeObject(apiResponse);
                var returnObj = JsonConvert.DeserializeObject<T>(res)!;
                return returnObj;
            }

            var returnVal = JsonConvert.SerializeObject(apiResponse);
            return JsonConvert.DeserializeObject<T>(returnVal)!;
        }
        catch (Exception e)
        {
            var dto = new APIResponse()
            {
                ErrorMessages = new List<string>() { e.Message.ToString() },
                IsSuccessful = false,
            };
            var res = JsonConvert.SerializeObject(dto);
            var apiResponse = JsonConvert.DeserializeObject<T>(res)!;

            return apiResponse;
        }
    }
}