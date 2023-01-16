using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VillaWebApp.Models.DTO;

namespace VillaWebApp.Models.ViewModels;

public class VillaNumberCreateVM
{
    public VillaNumberCreateDTO VillaNumber { get; set; } = new();
    [ValidateNever]
    public IEnumerable<SelectListItem> VillaList { get; set; } = null!;
}