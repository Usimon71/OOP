using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Tasks;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects.Subject;

public class ExamSubject
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public Guid AuthorId { get; private set; }

    public Guid? FatherId { get; private set; }

    public IList<EditableByAuthorTask> Tasks { get; private set; }

    public IList<EditableByAuthorLecture> Lectures { get; private set; }

    public double ExamPoints { get; }

    public ExamSubject(
        string name,
        Guid authorId,
        Guid? fatherId,
        IList<EditableByAuthorTask> tasks,
        IList<EditableByAuthorLecture> lectures,
        double examPoints)
    {
        Id = Guid.NewGuid();
        Name = name;
        AuthorId = authorId;
        FatherId = fatherId;
        Tasks = tasks;
        Lectures = lectures;
        ExamPoints = examPoints;
    }

    public void EditName(string newName)
    {
        Name = newName;
    }
}
