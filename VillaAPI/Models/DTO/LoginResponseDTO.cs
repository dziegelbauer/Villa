namespace VillaAPI.Models.DTO;

public class LoginResponseDTO
{
    public LocalUser? LocalUser { get; set; } = null;
    public string Token { get; set; } = String.Empty;
}