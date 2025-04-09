using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.ValidatedCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Writers;

namespace Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers.FileParameterHandlers;

public class FileShowParameterHandler : FileParameterHandler
{
    public override ParameterHandleResult? Handle(IEnumerator<string> request)
    {
        if (request.Current is not "show")
        {
            return NextFileParameterHandler?.Handle(request);
        }

        if (request.MoveNext() is false)
        {
            return new ParameterHandleResult.Failure("File show command must have parameters.");
        }

        string path = request.Current;
        string mode = "console";

        if (request.MoveNext() is false)
        {
            CommandBuildResult result = FileShowCommand.Build
                .WithWriter(new ConsoleWriter())
                .WithPath(path)
                .Build();

            if (result is CommandBuildResult.Success success)
            {
                return new ParameterHandleResult.Success(
                    new ValidateFileShowCommand((FileShowCommand)success.Command));
            }

            return ReturnSwitchResult(result);
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

        if (mode == "console")
        {
            CommandBuildResult result = FileShowCommand.Build
                .WithWriter(new ConsoleWriter())
                .WithPath(path)
                .Build();

            if (result is CommandBuildResult.Success success)
            {
                return new ParameterHandleResult.Success(
                    new ValidateFileShowCommand((FileShowCommand)success.Command));
            }

            return ReturnSwitchResult(result);
        }

        return new ParameterHandleResult.UnsupportedConnectionMode();
    }
}
