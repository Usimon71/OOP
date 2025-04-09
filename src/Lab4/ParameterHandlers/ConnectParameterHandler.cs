using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.ValidatedCommands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers;

public class ConnectParameterHandler : ParameterHandlerBase
{
    private readonly ConnectedFileSystem _fileSystem;

    public ConnectParameterHandler(ConnectedFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public override ParameterHandleResult? Handle(IEnumerator<string> request)
    {
        if (request.Current is not "connect")
        {
            return Next?.Handle(request);
        }

        if (request.MoveNext() is false)
        {
            return new ParameterHandleResult.Failure("Connect command must have parameters.");
        }

        string address = request.Current;
        string mode = "local";

        if (request.MoveNext() is false)
        {
            CommandBuildResult commandBuildResult = LocalConnectCommand.Build
                .WithFileSystem(_fileSystem)
                .WithPath(address)
                .Build();

            if (commandBuildResult is CommandBuildResult.Success success)
            {
                return new ParameterHandleResult.Success(
                    new ValidatedConnectCommand((LocalConnectCommand)success.Command));
            }

            return ReturnSwitchResult(commandBuildResult);
        }

        if (request.Current is not "-m")
        {
            return new ParameterHandleResult.Failure("Only -m parameter is supported.");
        }

        if (request.MoveNext() is false)
        {
            return new ParameterHandleResult.Failure("Mode parameter must have a value.");
        }

        mode = request.Current;

        if (mode != "local")
        {
            return new ParameterHandleResult.UnsupportedConnectionMode();
        }

        CommandBuildResult commandBuildResult2 = LocalConnectCommand.Build
            .WithFileSystem(_fileSystem)
            .WithPath(address)
            .Build();

        if (commandBuildResult2 is CommandBuildResult.Success success2)
        {
            return new ParameterHandleResult.Success(
                new ValidatedConnectCommand((LocalConnectCommand)success2.Command));
        }

        return ReturnSwitchResult(commandBuildResult2);
    }
}
