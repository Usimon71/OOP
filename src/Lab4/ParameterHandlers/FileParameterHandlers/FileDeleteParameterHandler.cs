using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.ValidatedCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers.FileParameterHandlers;

public class FileDeleteParameterHandler : FileParameterHandler
{
    public override ParameterHandleResult? Handle(IEnumerator<string> request)
    {
        if (request.Current is not "delete")
        {
            return NextFileParameterHandler?.Handle(request);
        }

        if (request.MoveNext() is false)
        {
            return new ParameterHandleResult.Failure("File delete command must have parameters.");
        }

        string path = request.Current;

        CommandBuildResult result = FileDeleteCommand.Build
            .WithPath(path)
            .Build();

        if (result is CommandBuildResult.Success success)
        {
            return new ParameterHandleResult.Success(
                new ValidateFileDeleteCommand((FileDeleteCommand)success.Command));
        }

        return ReturnSwitchResult(result);
    }
}
