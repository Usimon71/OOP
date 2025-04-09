using ICommand = Itmo.ObjectOrientedProgramming.Lab4.Commands.ICommand;

namespace Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers;

public abstract record ParameterHandleResult
{
    public sealed record Success(ICommand Command) : ParameterHandleResult;

    public sealed record Failure(string Message) : ParameterHandleResult;

    public sealed record UnsupportedConnectionMode() : ParameterHandleResult;
}
