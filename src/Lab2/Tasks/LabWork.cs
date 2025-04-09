namespace Itmo.ObjectOrientedProgramming.Lab2.Tasks;

public class LabWork : ITask
{
    public static Builder Build => new();

    public Guid Id { get; }

    public string Name { get; private set; }

    public Guid AuthorId { get; }

    public Guid? FatherId { get; }

    public double Points { get; }

    public string Description { get; private set; }

    public string Criteria { get; private set; }

    private LabWork(string name, Guid authorId, double points, string? description, string? criteria, Guid? fatherId)
    {
        Id = Guid.NewGuid();
        Name = name;
        AuthorId = authorId;
        Points = points;
        Description = description ?? string.Empty;
        Criteria = criteria ?? string.Empty;
        FatherId = fatherId;
    }

    public void EditName(string newName)
    {
        Name = newName;
    }

    public void EditDescription(string newDescription)
    {
        Description = newDescription;
    }

    public void EditCriteria(string newCriteria)
    {
        Criteria = newCriteria;
    }

    public ITask Clone()
    {
        return new LabWork(Name, AuthorId, Points, Description, Criteria, Id);
    }

    public class Builder :
        ITaskBuilder,
        ITaskNameSelector,
        ITaskAuthorSelector,
        ITaskPointsSelector
    {
        private string? _name;
        private Guid _authorId;
        private double _points;
        private string? _description;
        private string? _criteria;

        public ITaskAuthorSelector WithName(string name)
        {
            _name = name;

            return this;
        }

        public ITaskPointsSelector WithAuthor(Guid userId)
        {
            _authorId = userId;

            return this;
        }

        public ITaskBuilder WithPoints(double points)
        {
            _points = points;

            return this;
        }

        public ITaskBuilder WithDescription(string description)
        {
            _description = description;

            return this;
        }

        public ITaskBuilder WithCriteria(string criteria)
        {
            _criteria = criteria;

            return this;
        }

        public EditableByAuthorTask Build()
        {
            return new EditableByAuthorTask(
                new LabWork(
                    _name ?? throw new ArgumentNullException(nameof(_name)),
                    _authorId,
                    _points,
                    _description,
                    _criteria,
                    null));
        }
    }
}
