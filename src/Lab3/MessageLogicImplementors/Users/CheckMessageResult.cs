namespace Itmo.ObjectOrientedProgramming.Lab3.Addressees.Users;

public record CheckMessageResult
{
    public sealed record Success() : CheckMessageResult;

    public sealed record Failure(string Message) : CheckMessageResult;
}
