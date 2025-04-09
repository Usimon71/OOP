namespace Itmo.ObjectOrientedProgramming.Lab2.Tasks;

public class EditableByAuthorTask :
    IPrototype<EditableByAuthorTask>,
    IIdenticalEntity
{
    private readonly ITask _task;

    private bool AuthorIsNotTheSame(Guid authorId)
    {
        return _task.AuthorId != authorId;
    }

    public Guid Id => _task.Id;

    public string Name => _task.Name;

    public Guid AuthorId => _task.AuthorId;

    public Guid? FatherId => _task.FatherId;

    public double Points => _task.Points;

    public string? Description => _task.Description;

    public string? Criteria => _task.Criteria;

    public EditableByAuthorTask(ITask task)
    {
        _task = task;
    }

    public AuthorizedEditResult EditName(Guid authorId, string newName)
    {
        if (AuthorIsNotTheSame(authorId))
        {
            return new AuthorizedEditResult.Failure("You are not permitted to edit this task.");
        }

        _task.EditName(newName);

        return new AuthorizedEditResult.Success();
    }

    public AuthorizedEditResult EditDescription(Guid authorId, string newDescription)
    {
        if (AuthorIsNotTheSame(authorId))
        {
            return new AuthorizedEditResult.Failure("You are not permitted to edit this task.");
        }

        _task.EditDescription(newDescription);

        return new AuthorizedEditResult.Success();
    }

    public AuthorizedEditResult EditCriteria(Guid authorId, string newCriteria)
    {
        if (AuthorIsNotTheSame(authorId))
        {
            return new AuthorizedEditResult.Failure("You are not permitted to edit this task.");
        }

        _task.EditCriteria(newCriteria);

        return new AuthorizedEditResult.Success();
    }

    public EditableByAuthorTask Clone()
    {
        return new EditableByAuthorTask(_task.Clone());
    }
}
