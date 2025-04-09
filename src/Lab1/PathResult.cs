namespace Itmo.ObjectOrientedProgramming.Lab1;

public abstract record PathResult
{
    private PathResult() { }

    public sealed record Success(double TotalTime) : PathResult;

    public sealed record Failure(string Message) : PathResult;
}
