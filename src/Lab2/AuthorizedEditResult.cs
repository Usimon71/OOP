namespace Itmo.ObjectOrientedProgramming.Lab2;

public abstract record AuthorizedEditResult
{
    private AuthorizedEditResult() { }

    public sealed record Success : AuthorizedEditResult;

    public sealed record Failure(string Message) : AuthorizedEditResult;
}
