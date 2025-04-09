using Application.Abstractions.Repositories;
using Application.Contracts.Accounts;
using Application.Contracts.Results;
using Application.Models.Operations;

namespace Application.Application.Accounts;

public class AccountOperationsService : IAccountOperationsService
{
    private readonly AccountService _accountService;
    private readonly IOperationRepository _operationRepository;

    public AccountOperationsService(AccountService accountService, IOperationRepository operationRepository)
    {
        _accountService = accountService;
        _operationRepository = operationRepository;
    }

    public WithdrawResult WithdrawFromAccountNumber(long amount)
    {
        WithdrawResult result = _accountService.WithdrawFromAccountNumber(amount);

        if (result is WithdrawResult.Success success)
        {
            Operation operation = Operation.Build
                .WithAccountNumber(_accountService.CurrentAccountManager.Account?.Number)
                .WithOperationType(OperationType.Withdraw)
                .WithTimeStamp(DateTime.UtcNow)
                .Build();
            _operationRepository.AddOperation(operation);

            return success;
        }

        return result;
    }

    public ReplenishResult ReplenishAccountNumber(long amount)
    {
        ReplenishResult result = _accountService.ReplenishAccountNumber(amount);

        if (result is ReplenishResult.Success success)
        {
            Operation operation = Operation.Build
                .WithAccountNumber(_accountService.CurrentAccountManager.Account?.Number)
                .WithOperationType(OperationType.Replenish)
                .WithTimeStamp(DateTime.UtcNow)
                .Build();
            _operationRepository.AddOperation(operation);

            return success;
        }

        return result;
    }

    public BalanceCheckResult BalanceCheckAccountNumber()
    {
        BalanceCheckResult result = _accountService.BalanceCheckAccountNumber();

        if (result is BalanceCheckResult.Success success)
        {
            Operation operation = Operation.Build
                .WithAccountNumber(_accountService.CurrentAccountManager.Account?.Number)
                .WithOperationType(OperationType.BalanceCheck)
                .WithTimeStamp(DateTime.UtcNow)
                .Build();
            _operationRepository.AddOperation(operation);

            return success;
        }

        return result;
    }

    public IEnumerable<Operation> GetOperations()
    {
        if (_accountService.CurrentAccountManager.Account is null)
        {
            throw new InvalidOperationException("Current account is null");
        }

        return _operationRepository.FindOperationsByAccountNumber(
            _accountService.CurrentAccountManager.Account.Number) ?? [];
    }

    public LoginResult Login(long accountNumber, int pin)
    {
        return _accountService.Login(accountNumber, pin);
    }

    public void Logout()
    {
        _accountService.Logout();
    }
}
