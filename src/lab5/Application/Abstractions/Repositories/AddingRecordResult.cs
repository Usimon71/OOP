namespace Application.Abstractions.Repositories;

public record AddingRecordResult
{
    public sealed record Success : AddingRecordResult;

    public sealed record AlreadyExists : AddingRecordResult;
}
