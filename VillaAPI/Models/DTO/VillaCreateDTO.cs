using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models.DTO;

public class VillaCreateDTO
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; } = String.Empty;
    public string Details { get; set; } = String.Empty;
    [Required]
    public double Rate { get; set; }
    public int Occupancy { get; set; }
    public int Sqft { get; set; }
    public string ImageUrl { get; set; } = String.Empty;
    public string Amenity { get; set; }= String.Empty;
}