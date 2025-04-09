using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.ValidatedCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers.TreeParameterHandlers;

public class TreeGotoParameterHandler : TreeParameterHandler
{
    public override ParameterHandleResult? Handle(IEnumerator<string> request)
    {
        if (request.Current is not "goto")
        {
            return NextTreeParameterHandler?.Handle(request);
        }

        if (request.MoveNext() is false)
        {
            return new ParameterHandleResult.Failure("Goto command must have path parameter.");
        }

        string path = request.Current;

        CommandBuildResult result = LocalTreeGotoCommand.Build
            .WithNewPath(path)
            .Build();

        if (result is CommandBuildResult.Success success)
        {
            return new ParameterHandleResult.Success(
                new ValidateTreeGotoCommand((LocalTreeGotoCommand)success.Command));
        }

        return ReturnSwitchResult(result);
    }
}
