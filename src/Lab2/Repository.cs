namespace Itmo.ObjectOrientedProgramming.Lab2;

public class Repository<T> where T : IIdenticalEntity
{
    private static HashSet<T> Entities { get; } = [];

    public void AddEntity(T entity)
    {
        Entities.Add(entity);
    }

    public T GetEntity(Guid entityId)
    {
        T? entity = Entities.FirstOrDefault(entity => entity.Id == entityId) ??
        throw new ArgumentException($"Entity with Id {entityId} not found");

        return entity;
    }
}
