namespace VillaWebApp.Models.DTO;

public class UserDTO
{
    public int Id { get; set; }
    public string UserName { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string Role { get; set; } = String.Empty;
}