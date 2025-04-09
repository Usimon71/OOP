using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.ValidatedCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers.FileParameterHandlers;

public class FileCopyParameterHandler : FileParameterHandler
{
    public override ParameterHandleResult? Handle(IEnumerator<string> request)
    {
        if (request.Current is not "copy")
        {
            return NextFileParameterHandler?.Handle(request);
        }

        if (request.MoveNext() is false)
        {
            return new ParameterHandleResult.Failure("File copy command must have source path parameter.");
        }

        string sourcePath = request.Current;
        string targetPath = request.MoveNext() is false ? string.Empty : request.Current;

        CommandBuildResult result = FileCopyCommand.Build
            .WithSourcePath(sourcePath)
            .WithTargetPath(targetPath)
            .Build();

        if (result is CommandBuildResult.Success success)
        {
            return new ParameterHandleResult.Success(
                new ValidateFileCopyCommand((FileCopyCommand)success.Command));
        }

        return ReturnSwitchResult(result);
    }
}
