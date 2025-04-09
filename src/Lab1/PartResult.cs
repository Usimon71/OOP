namespace Itmo.ObjectOrientedProgramming.Lab1;

public abstract record PartResult
{
    private PartResult() { }

    public sealed record Success() : PartResult;

    public sealed record Failure(string Message) : PartResult;
}
