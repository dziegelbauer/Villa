namespace VillaAPI.Models;

public class Villa
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public DateTime Created { get; set; } = DateTime.Now;
}