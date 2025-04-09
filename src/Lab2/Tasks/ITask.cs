namespace Itmo.ObjectOrientedProgramming.Lab2.Tasks;

public interface ITask : IPrototype<ITask>
{
    public Guid Id { get; }

    public string Name { get; }

    public Guid AuthorId { get; }

    public Guid? FatherId { get; }

    public double Points { get; }

    public string? Description { get; }

    public string? Criteria { get; }

    public void EditName(string newName);

    public void EditDescription(string newDescription);

    public void EditCriteria(string newCriteria);
}
