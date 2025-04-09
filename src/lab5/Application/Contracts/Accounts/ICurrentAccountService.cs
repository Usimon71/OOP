using Application.Models.Accounts;

namespace Application.Contracts.Accounts;

public interface ICurrentAccountService
{
    Account? Account { get; }
}
