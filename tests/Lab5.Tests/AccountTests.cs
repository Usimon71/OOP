using Application.Abstractions.Repositories;
using Application.Application.Accounts;
using Application.Contracts.Results;
using Application.Models.Accounts;
using Moq;
using Xunit;

namespace Lab5.Tests;

public class AccountTests
{
    [Fact]
    public void WithdrawSuccessTest()
    {
        // Arrange
        const int initialBalance = 100500;
        const int withdrawAmount = 500;
        (Mock<IAccountRepository> repository, AccountService accountService) = CreateAccountService(initialBalance);

        // Act
        WithdrawResult result = accountService.WithdrawFromAccountNumber(withdrawAmount);
        BalanceCheckResult resultBalance = accountService.BalanceCheckAccountNumber();

        // Assert
        Assert.True(result is WithdrawResult.Success);
        Assert.Equal(initialBalance - withdrawAmount, Assert.IsType<BalanceCheckResult.Success>(resultBalance).Balance);
    }

    [Fact]
    public void WithdrawInsufficientFundsTest()
    {
        // Arrange
        const int initialBalance = 100500;
        const int withdrawAmount = 100501;
        (Mock<IAccountRepository> repository, AccountService accountService) = CreateAccountService(initialBalance);

        // Act
        WithdrawResult result = accountService.WithdrawFromAccountNumber(withdrawAmount);

        // Assert
        Assert.True(result is WithdrawResult.InsufficientFunds);
    }

    [Fact]
    public void ReplenishSuccessTest()
    {
        // Arrange
        const int initialBalance = 100000;
        const int replenishAmount = 500;
        (Mock<IAccountRepository> repository, AccountService accountService) = CreateAccountService(initialBalance);

        // Act
        ReplenishResult result = accountService.ReplenishAccountNumber(replenishAmount);
        BalanceCheckResult resultBalance = accountService.BalanceCheckAccountNumber();

        // Assert
        Assert.True(result is ReplenishResult.Success);
        Assert.Equal(initialBalance + replenishAmount, Assert.IsType<BalanceCheckResult.Success>(resultBalance).Balance);
    }

    private (Mock<IAccountRepository> Repository, AccountService Service) CreateAccountService(int initialBalance)
    {
        var repository = new Mock<IAccountRepository>();
        var accountManager = new CurrentAccountManager
        {
            Account = new Account(1, 1, 1234, initialBalance),
        };
        var accountService = new AccountService(repository.Object, accountManager);

        repository.Setup(repo => repo.FindBalanceByAccountNumber(1))
            .Returns(() => Assert.IsType<Account>(accountService.CurrentAccountManager.Account).Balance);
        repository.Setup(repo => repo.SetBalanceForAccount(1, It.IsAny<long>()))
            .Callback<long, long?>((accountNumber, newBalanceValue) =>
            {
                if (newBalanceValue.HasValue)
                {
                    accountService.CurrentAccountManager.Account = Assert.IsType<Account>(accountService.CurrentAccountManager.Account) with
                    {
                        Balance = newBalanceValue.Value,
                    };
                }
            });

        return (repository, accountService);
    }
}
