namespace Itmo.ObjectOrientedProgramming.Lab2.Tasks;

public interface ITaskBuilder
{
    ITaskBuilder WithDescription(string description);

    ITaskBuilder WithCriteria(string criteria);

    EditableByAuthorTask Build();
}

public interface ITaskNameSelector
{
    ITaskAuthorSelector WithName(string name);
}

public interface ITaskAuthorSelector
{
    ITaskPointsSelector WithAuthor(Guid userId);
}

public interface ITaskPointsSelector
{
    ITaskBuilder WithPoints(double points);
}
