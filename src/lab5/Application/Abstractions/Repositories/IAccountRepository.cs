using Application.Models.Accounts;

namespace Application.Abstractions.Repositories;

public interface IAccountRepository
{
    void SetBalanceForAccount(long number, long? value);

    Account? FindAccountByNumber(long number);

    long? FindBalanceByAccountNumber(long number);
}
