using Application.Models.Operations;

namespace Application.Abstractions.Repositories;

public interface IOperationRepository
{
    IEnumerable<Operation>? FindOperationsByAccountNumber(long accountNumber);

    AddingRecordResult AddOperation(Operation operation);
}
