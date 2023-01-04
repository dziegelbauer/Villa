namespace VillaAPI.Logging;

public class Loggingv2 : ILogging
{
    public void Log(string message, string type)
    {
        switch (type)
        {
            case "error":
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR - {message}");
                Console.BackgroundColor = ConsoleColor.Black;
                break;
            case "warning":
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"WARNING - {message}");
                Console.BackgroundColor = ConsoleColor.Black;
                break;
            default:
                Console.WriteLine(message);
                break;
        }
    }
}