using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VillaWebApp.Models;
using VillaWebApp.Models.DTO;
using VillaWebApp.Services.IServices;

namespace VillaWebApp.Controllers;

public class VillaController : Controller
{
    private readonly IVillaService _villaService;
    private readonly IMapper _mapper;

    public VillaController(IVillaService villaService, IMapper mapper)
    {
        _villaService = villaService;
        _mapper = mapper;
    }
    
    // GET
    public async Task<IActionResult> IndexVilla()
    {
        List<VillaDTO> list = new();

        var response = await _villaService.GetAllAsync<APIResponse>();

        if (response?.Result is not null && response.IsSuccessful)
        {
            list = JsonConvert.DeserializeObject<List<VillaDTO>>(response.Result.ToString()!) ?? new();
        }
        
        return View(list);
    }
    
    public IActionResult CreateVilla()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateVilla(VillaCreateDTO model)
    {
        if (ModelState.IsValid)
        {
            var response = await _villaService.CreateAsync<APIResponse>(model);
            if (response?.Result is not null && response.IsSuccessful)
            {
                return RedirectToAction(nameof(IndexVilla));
            }
        }
        
        return View(model);
    }
    
    public async Task<IActionResult> UpdateVilla(int id)
    {
        List<VillaDTO> list = new();

        var response = await _villaService.GetAllAsync<APIResponse>();

        if (response?.Result is not null && response.IsSuccessful)
        {
            list = JsonConvert.DeserializeObject<List<VillaDTO>>(response.Result.ToString()!) ?? new();
        }
        
        return View(list);
    }
    
    public async Task<IActionResult> DeleteVilla(int id)
    {
        List<VillaDTO> list = new();

        var response = await _villaService.GetAllAsync<APIResponse>();

        if (response?.Result is not null && response.IsSuccessful)
        {
            list = JsonConvert.DeserializeObject<List<VillaDTO>>(response.Result.ToString()!) ?? new();
        }
        
        return View(list);
    }
}