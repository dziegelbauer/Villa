﻿using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models.DTO;

public class VillaUpdateDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string Name { get; set; } = String.Empty;
    public string? Details { get; set; } = String.Empty;
    [Required]
    public double Rate { get; set; }
    [Required]
    public int Occupancy { get; set; }
    [Required]
    public int Sqft { get; set; }
    [Required]
    public string ImageUrl { get; set; } = String.Empty;
    public string? Amenity { get; set; } = String.Empty;
}