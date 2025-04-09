namespace Application.Contracts.Results;

public record BalanceCheckResult
{
    public sealed record Success(long Balance) : BalanceCheckResult;

    public sealed record NotFound : BalanceCheckResult;
}
