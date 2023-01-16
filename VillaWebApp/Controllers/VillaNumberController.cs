﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using VillaWebApp.Models;
using VillaWebApp.Models.DTO;
using VillaWebApp.Models.ViewModels;
using VillaWebApp.Services.IServices;

namespace VillaWebApp.Controllers;

public class VillaNumberController : Controller
{
    private readonly IVillaNumberService _villaNumberService;
    private readonly IVillaService _villaService;
    private readonly IMapper _mapper;

    public VillaNumberController(IVillaNumberService villaNumberService, IVillaService villaService, IMapper mapper)
    {
        _villaNumberService = villaNumberService;
        _villaService = villaService;
        _mapper = mapper;
    }
    
    // GET
    public async Task<IActionResult> IndexVillaNumber()
    {
        List<VillaNumberDTO> list = new();

        var response = await _villaNumberService.GetAllAsync<APIResponse>();

        if (response?.Result is not null && response.IsSuccessful)
        {
            list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(response.Result.ToString()!) ?? new();
        }
        
        return View(list);
    }
    
    public async Task<IActionResult> UpdateVillaNumber(int id)
    {
        var response = await _villaNumberService.GetAsync<APIResponse>(id);

        if (response?.Result is not null && response.IsSuccessful)
        {
            VillaNumberUpdateVM vm = new();
            
            VillaNumberDTO? model = JsonConvert.DeserializeObject<VillaNumberDTO>(response.Result.ToString()!);
            vm.VillaNumber = _mapper.Map<VillaNumberUpdateDTO>(model);
            
            if (response?.Result is not null && response.IsSuccessful)
            {
                vm.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(response.Result.ToString()!)!.Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
            }

            return View(vm);
        }
        
        return NotFound();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM model)
    {
        if (ModelState.IsValid)
        {
            var response = await _villaNumberService.UpdateAsync<APIResponse>(model.VillaNumber);
            if (response?.Result is not null && response.IsSuccessful)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }
        }
        
        return View(model);
    }
    
    public async Task<IActionResult> CreateVillaNumber()
    {
        var response = await _villaService.GetAllAsync<APIResponse>();

        VillaNumberCreateVM vm = new();
        
        if (response?.Result is not null && response.IsSuccessful)
        {
            vm.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(response.Result.ToString()!)!.Select(u => new SelectListItem()
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });;
        }

        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
    {
        if (ModelState.IsValid)
        {
            var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber);
            if (response?.Result is not null && response.IsSuccessful)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }
        }
        
        return View(model);
    }
    
    public async Task<IActionResult> DeleteVillaNumber(int id)
    {
        var response = await _villaNumberService.GetAsync<APIResponse>(id);

        if (response?.Result is not null && response.IsSuccessful)
        {
            VillaNumberDTO? model = JsonConvert.DeserializeObject<VillaNumberDTO>(response.Result.ToString()!);
            return View(_mapper.Map<VillaNumberDTO>(model));
        }
        
        return NotFound();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteVillaNumber(VillaNumberDTO model)
    {
        if (ModelState.IsValid)
        {
            var response = await _villaNumberService.DeleteAsync<APIResponse>(model.VillaNo);
            if (response.IsSuccessful)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }
        }
        
        return View(model);
    }
}