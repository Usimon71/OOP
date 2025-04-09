namespace Application.Abstractions.Repositories;

public interface ISystemRepository
{
    int? GetPasswordHash();
}
