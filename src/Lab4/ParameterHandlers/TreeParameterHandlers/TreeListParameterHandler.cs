using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.ValidatedCommands;
using Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers.FlagParameterHandlers;

namespace Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers.TreeParameterHandlers;

public class TreeListParameterHandler : TreeParameterHandler
{
    public override ParameterHandleResult? Handle(IEnumerator<string> request)
    {
        if (request.Current is not "list")
        {
            return NextTreeParameterHandler?.Handle(request);
        }

        if (request.MoveNext() is false)
        {
            CommandBuildResult result = LocalTreeListCommand.Build
                .WithMaxDepth(1)
                .WithOutputMode("console")
                .Build();

            if (result is CommandBuildResult.Success success)
            {
                return new ParameterHandleResult.Success(
                    new ValidateTreeListCommand((LocalTreeListCommand)success.Command));
            }

            return ReturnSwitchResult(result);
        }

        var parameterDictionary = new Dictionary<string, string>
        {
            { "-m", "console" },
            { "-d", "1" },
        };

        var modeHandler = new FlagParameterHandler("-m");
        modeHandler.AddNext(new FlagParameterHandler("-d"));
        modeHandler.Handle(request, parameterDictionary);

        if (int.TryParse(parameterDictionary["-d"], out int depth) is false)
        {
            return new ParameterHandleResult.Failure("Depth must be a number.");
        }

        if (parameterDictionary["-m"] == "console")
        {
            CommandBuildResult result = LocalTreeListCommand.Build
                .WithMaxDepth(depth)
                .WithOutputMode("console")
                .Build();

            if (result is CommandBuildResult.Success success)
            {
                return new ParameterHandleResult.Success(
                    new ValidateTreeListCommand((LocalTreeListCommand)success.Command));
            }

            return ReturnSwitchResult(result);
        }

        return new ParameterHandleResult.Failure("Unsupported output mode.");
    }
}
