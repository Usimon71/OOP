namespace Application.Contracts.Results;

public record ReplenishResult
{
    public sealed record Success : ReplenishResult;

    public sealed record AccountNotFound : ReplenishResult;
}
