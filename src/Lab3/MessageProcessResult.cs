namespace Itmo.ObjectOrientedProgramming.Lab3;

public record MessageProcessResult
{
    public sealed record Success : MessageProcessResult;

    public sealed record Failure(string Message) : MessageProcessResult;
}
