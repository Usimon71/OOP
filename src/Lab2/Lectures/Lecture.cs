namespace Itmo.ObjectOrientedProgramming.Lab2.Lectures;

public class Lecture
{
    public Guid Id { get; }

    public string Name { get; private set; }

    public Guid AuthorId { get; }

    public Guid? FatherId { get; }

    public string Description { get; private set; }

    public string Content { get; private set; }

    internal Lecture(
        string name,
        Guid authorId,
        string? description,
        string? content,
        Guid? fatherId)
    {
        Id = Guid.NewGuid();
        Name = name;
        AuthorId = authorId;
        Description = description ?? string.Empty;
        Content = content ?? string.Empty;
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

    public void EditContent(string newContent)
    {
        Content = newContent;
    }

    public Lecture Clone()
    {
        return new Lecture(Name, AuthorId, Description, Content, Id);
    }
}
