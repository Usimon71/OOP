using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.RequestHandlers;

namespace Itmo.ObjectOrientedProgramming.Lab4.UserInterfaces;

public class ConsoleUserInterface : IUserInterface
{
    private readonly LocalRequestHandler _handler;
    private readonly ConnectedFileSystem _fileSystem;

    public ConsoleUserInterface()
    {
        _fileSystem = new ConnectedFileSystem(
            new LocalFileSystem());
        _handler = new LocalRequestHandler(_fileSystem);
    }

    public void Run()
    {
        string? line;
        Console.Write("Enter command: ");
        while ((line = Console.ReadLine()) != null && !line.Equals("exit", StringComparison.Ordinal))
        {
            ParameterHandleResult? result = _handler.HandleRequest(line);
            switch (result)
            {
                case null:
                    Console.WriteLine("\nCommand not found\n");
                    break;
                case ParameterHandleResult.Success success:
                    success.Command.Execute(_fileSystem);
                    Console.WriteLine("\nDone!\n");
                    break;
                case ParameterHandleResult.Failure failure:
                    Console.WriteLine("\nFailure: " + failure.Message + "\n");
                    break;
                case ParameterHandleResult.UnsupportedConnectionMode:
                    Console.WriteLine("\nUnsupported connection mode" + "\n");
                    break;
            }

            Console.Write("Enter command: ");
        }
    }
}
