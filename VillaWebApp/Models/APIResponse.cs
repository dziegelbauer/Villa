using System.Net;

namespace VillaWebApp.Models;

public class APIResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccessful { get; set; }
    public List<string>? ErrorMessages { get; set; } = null;
    public object? Result { get; set; } = null;
}