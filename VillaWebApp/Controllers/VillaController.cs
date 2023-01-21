using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_Utility;
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

        var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessionToken)!);

        if (response?.Result is not null && response.IsSuccessful)
        {
            list = JsonConvert.DeserializeObject<List<VillaDTO>>(response.Result.ToString()!) ?? new();
        }
        
        return View(list);
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateVilla(int id)
    {
        var response = await _villaService.GetAsync<APIResponse>(id, HttpContext.Session.GetString(StaticDetails.SessionToken)!);

        if (response?.Result is not null && response.IsSuccessful)
        {
            VillaDTO? model = JsonConvert.DeserializeObject<VillaDTO>(response.Result.ToString()!);
            return View(_mapper.Map<VillaUpdateDTO>(model));
        }
        
        return NotFound();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateVilla(VillaUpdateDTO model)
    {
        if (ModelState.IsValid)
        {
            var response = await _villaService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(StaticDetails.SessionToken)!);
            if (response?.Result is not null && response.IsSuccessful)
            {
                TempData["success"] = "Villa updated successfully.";
                return RedirectToAction(nameof(IndexVilla));
            }
        }
        
        TempData["error"] = "Villa update failed.";
        return View(model);
    }
    
    [Authorize(Roles = "Admin")]
    public IActionResult CreateVilla()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateVilla(VillaCreateDTO model)
    {
        if (ModelState.IsValid)
        {
            var response = await _villaService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(StaticDetails.SessionToken)!);
            if (response?.Result is not null && response.IsSuccessful)
            {
                TempData["success"] = "Villa created successfully.";
                return RedirectToAction(nameof(IndexVilla));
            }
        }
        
        TempData["error"] = "Villa creation failed.";
        return View(model);
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteVilla(int id)
    {
        var response = await _villaService.GetAsync<APIResponse>(id, HttpContext.Session.GetString(StaticDetails.SessionToken)!);

        if (response?.Result is not null && response.IsSuccessful)
        {
            VillaDTO? model = JsonConvert.DeserializeObject<VillaDTO>(response.Result.ToString()!);
            return View(_mapper.Map<VillaDTO>(model));
        }
        
        return NotFound();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteVilla(VillaDTO model)
    {
        if (ModelState.IsValid)
        {
            var response = await _villaService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(StaticDetails.SessionToken)!);
            if (response.IsSuccessful)
            {
                TempData["success"] = "Villa deleted successfully.";
                return RedirectToAction(nameof(IndexVilla));
            }
        }
        
        TempData["error"] = "Villa deletion failed.";
        return View(model);
    }
}