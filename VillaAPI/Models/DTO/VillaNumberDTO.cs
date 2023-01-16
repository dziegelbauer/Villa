using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models.DTO;

public class VillaNumberDTO
{
    [Required]
    public int VillaNo { get; set; }
    [Required] 
    public int VillaId { get; set; }
    public string SpecialDetails { get; set; } = String.Empty;
    public VillaDTO? Villa { get; set; } = null;
}