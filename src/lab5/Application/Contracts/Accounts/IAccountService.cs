using Application.Contracts.Results;

namespace Application.Contracts.Accounts;

public interface IAccountService
{
    WithdrawResult WithdrawFromAccountNumber(long amount);

    ReplenishResult ReplenishAccountNumber(long amount);

    BalanceCheckResult BalanceCheckAccountNumber();

    LoginResult Login(long accountNumber, int pin);

    void Logout();
}
