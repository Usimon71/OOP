using Itmo.ObjectOrientedProgramming.Lab3.Addressees.Users;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageLogicImplementors;

public class ConsoleWriter : IConsoleWriter
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
}
