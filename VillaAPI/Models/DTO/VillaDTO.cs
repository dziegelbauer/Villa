﻿using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models.DTO;

public class VillaDTO
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(30)]
    public string Name { get; set; } = String.Empty;

    public int Occupancy { get; set; }

    public int Sqft { get; set; }
}