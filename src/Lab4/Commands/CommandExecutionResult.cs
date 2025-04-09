namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public record CommandExecutionResult
{
    public sealed record Success : CommandExecutionResult;

    public sealed record Failure(string Message) : CommandExecutionResult;
}
