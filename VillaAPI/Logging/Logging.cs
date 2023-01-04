namespace VillaAPI.Logging;

public class Logging : ILogging
{
    public void Log(string message, string type)
    {
        if (String.Equals(type, "error"))
        {
            Console.WriteLine($"ERROR - {message}");
        }
        else
        {
            Console.WriteLine(message);
        }
    }
}