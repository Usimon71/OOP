namespace Application.Contracts.Results;

public record WithdrawResult
{
    public sealed record Success : WithdrawResult;

    public sealed record AccountNotFound : WithdrawResult;

    public sealed record InsufficientFunds : WithdrawResult;
}
