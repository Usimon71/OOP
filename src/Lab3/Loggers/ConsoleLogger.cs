namespace Itmo.ObjectOrientedProgramming.Lab3.Loggers;

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}
