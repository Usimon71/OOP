using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers;

public abstract class ParameterHandlerBase : IParameterHandler
{
    protected IParameterHandler? Next { get; private set; }

    public IParameterHandler AddNext(IParameterHandler handler)
    {
        if (Next is null)
        {
            Next = handler;
        }
        else
        {
            Next.AddNext(handler);
        }

        return this;
    }

    public abstract ParameterHandleResult? Handle(IEnumerator<string> request);

    protected static ParameterHandleResult ReturnSwitchResult(CommandBuildResult result)
    {
        return result switch
        {
            CommandBuildResult.InvalidPath => new ParameterHandleResult.Failure("Invalid path."),
            CommandBuildResult.UnsupportedMode => new ParameterHandleResult.UnsupportedConnectionMode(),
            CommandBuildResult.DisconnectedFileSystem => new ParameterHandleResult.Failure("Filesystem is disconnected"),
            _ => throw new InvalidOperationException(),
        };
    }
}
