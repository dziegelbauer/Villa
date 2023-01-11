using System.ComponentModel.DataAnnotations;

namespace VillaWebApp.Models.DTO;

public class VillaNumberUpdateDTO
{
    [Required]
    public int VillaNo { get; set; }
    [Required] 
    public int VillaId { get; set; }
    public string SpecialDetails { get; set; } = String.Empty;
}