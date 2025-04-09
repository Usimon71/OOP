using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.ValidatedCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers.FileParameterHandlers;

public class FileRenameParameterHandler : FileParameterHandler
{
    public override ParameterHandleResult? Handle(IEnumerator<string> request)
    {
        if (request.Current is not "rename")
        {
            return NextFileParameterHandler?.Handle(request);
        }

        if (request.MoveNext() is false)
        {
            return new ParameterHandleResult.Failure("File rename command must have path parameter.");
        }

        string path = request.Current;

        if (request.MoveNext() is false)
        {
            return new ParameterHandleResult.Failure("File rename command must have new name parameter.");
        }

        string newName = request.Current;
        CommandBuildResult result = FileRenameCommand.Build
            .WithPath(path)
            .WithNewFileName(newName)
            .Build();

        if (result is CommandBuildResult.Success success)
        {
            return new ParameterHandleResult.Success(
                new ValidateFileRenameCommand((FileRenameCommand)success.Command));
        }

        return ReturnSwitchResult(result);
    }
}
