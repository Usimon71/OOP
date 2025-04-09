using Itmo.ObjectOrientedProgramming.Lab4.UserInterfaces;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public class Program
{
    public static void Main(string[] args)
    {
        var interfaceObject = new ConsoleUserInterface();
        interfaceObject.Run();
    }
}
