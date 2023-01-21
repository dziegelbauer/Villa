using Villa_Utility;

namespace VillaWebApp.Models
{
    public class APIRequest
    {
        public StaticDetails.ApiType ApiType { get; set; }
        public string Url { get; set; } = String.Empty;
        public object? Data { get; set; } = null;
        public string Token { get; set; }
    }
}
