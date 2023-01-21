namespace VillaWebApp.Models.DTO;

public class LoginResponseDTO
{
    public UserDTO? LocalUser { get; set; } = null;
    public string Token { get; set; } = String.Empty;
}