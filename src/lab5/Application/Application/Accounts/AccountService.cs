using Application.Abstractions.Repositories;
using Application.Contracts.Accounts;
using Application.Contracts.Results;
using Application.Models.Accounts;

namespace Application.Application.Accounts;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public CurrentAccountManager CurrentAccountManager { get; private set; }

    public AccountService(
        IAccountRepository accountRepository,
        CurrentAccountManager currentAccountManager)
    {
        _accountRepository = accountRepository;
        CurrentAccountManager = currentAccountManager;
    }

    public WithdrawResult WithdrawFromAccountNumber(long amount)
    {
        if (CurrentAccountManager.Account is null)
        {
            throw new InvalidOperationException("Current account is null");
        }

        long? balance = _accountRepository.FindBalanceByAccountNumber(CurrentAccountManager.Account.Number);
        if (balance is null)
        {
            return new WithdrawResult.AccountNotFound();
        }

        if (balance - amount < 0)
        {
            return new WithdrawResult.InsufficientFunds();
        }

        _accountRepository.SetBalanceForAccount(CurrentAccountManager.Account.Number, balance - amount);

        return new WithdrawResult.Success();
    }

    public ReplenishResult ReplenishAccountNumber(long amount)
    {
        if (CurrentAccountManager.Account is null)
        {
            throw new InvalidOperationException("Current account is null");
        }

        long? balance = _accountRepository.FindBalanceByAccountNumber(CurrentAccountManager.Account.Number);
        if (balance is null)
        {
            return new ReplenishResult.AccountNotFound();
        }

        _accountRepository.SetBalanceForAccount(CurrentAccountManager.Account.Number, balance + amount);

        return new ReplenishResult.Success();
    }

    public BalanceCheckResult BalanceCheckAccountNumber()
    {
        if (CurrentAccountManager.Account is null)
        {
            throw new InvalidOperationException("Current account is null");
        }

        long? balance = _accountRepository.FindBalanceByAccountNumber(CurrentAccountManager.Account.Number);
        if (balance is null)
        {
            return new BalanceCheckResult.NotFound();
        }

        return new BalanceCheckResult.Success(balance.Value);
    }

    public LoginResult Login(long accountNumber, int pin)
    {
        Account? account = _accountRepository.FindAccountByNumber(accountNumber);
        if (account is null)
        {
            return new LoginResult.AccountNotFound();
        }

        if (pin != account.Pin)
        {
            return new LoginResult.WrongPin();
        }

        CurrentAccountManager.Account = account;

        return new LoginResult.Success();
    }

    public void Logout()
    {
        CurrentAccountManager.Account = null;
    }
}
