namespace VillaWebApp.Models.DTO;

public class RegistrationRequestDTO
{
    public string UserName { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string Role { get; set; } = String.Empty;
}