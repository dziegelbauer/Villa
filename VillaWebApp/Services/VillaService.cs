﻿using Villa_Utility;
using VillaWebApp.Models;
using VillaWebApp.Models.DTO;
using VillaWebApp.Services.IServices;

namespace VillaWebApp.Services;

public class VillaService : BaseService, IVillaService
{
    private readonly string _apiUrl;

    public VillaService(IHttpClientFactory httpClientFactory, IConfiguration configuration) 
        : base(httpClientFactory)
    {
        _apiUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI")!;
    }
    
    public async Task<T> GetAsync<T>(int id)
    {
        return await SendAsync<T>(new APIRequest()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = $"{_apiUrl}/api/VillaAPI/{id}",
            Data = null,
        });
    }

    public async Task<T> GetAllAsync<T>()
    {
        return await SendAsync<T>(new APIRequest()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = $"{_apiUrl}/api/VillaAPI",
            Data = null,
        });
    }

    public async Task<T> CreateAsync<T>(VillaCreateDTO dto)
    {
        return await SendAsync<T>(new APIRequest()
        {
            ApiType = StaticDetails.ApiType.POST,
            Url = $"{_apiUrl}/api/VillaAPI",
            Data = dto
        });
    }

    public async Task<T> UpdateAsync<T>(VillaUpdateDTO dto)
    {
        return await SendAsync<T>(new APIRequest()
        {
            ApiType = StaticDetails.ApiType.PUT,
            Url = $"{_apiUrl}/api/VillaAPI/{dto.Id}",
            Data = dto
        });
    }

    public async Task<T> DeleteAsync<T>(int id)
    {
        return await SendAsync<T>(new APIRequest()
        {
            ApiType = StaticDetails.ApiType.DELETE,
            Url = $"{_apiUrl}/api/VillaAPI/{id}",
            Data = null,
        });
    }
}