using Application.Contracts.Results;
using Application.Models.Operations;

namespace Application.Contracts.Accounts;

public interface IAccountOperationsService
{
    WithdrawResult WithdrawFromAccountNumber(long amount);

    ReplenishResult ReplenishAccountNumber(long amount);

    BalanceCheckResult BalanceCheckAccountNumber();

    IEnumerable<Operation> GetOperations();

    LoginResult Login(long accountNumber, int pin);

    void Logout();
}
