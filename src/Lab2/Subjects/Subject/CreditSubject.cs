using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Tasks;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects.Subject;

public class CreditSubject
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public Guid AuthorId { get; private set; }

    public Guid? FatherId { get; private set; }

    public IList<EditableByAuthorTask> Tasks { get; private set; }

    public IList<EditableByAuthorLecture> Lectures { get; private set; }

    public double MinCreditPoints { get; private set; }

    public CreditSubject(
        string name,
        Guid authorId,
        Guid? fatherId,
        IList<EditableByAuthorTask> tasks,
        IList<EditableByAuthorLecture> lectures,
        double minCreditPoints)
    {
        Id = Guid.NewGuid();
        Name = name;
        AuthorId = authorId;
        FatherId = fatherId;
        Tasks = tasks;
        Lectures = lectures;
        MinCreditPoints = minCreditPoints;
    }

    public void EditName(string newName)
    {
        Name = newName;
    }

    public void EditMinCreditPoints(double newMinCreditPoints)
    {
        MinCreditPoints = newMinCreditPoints;
    }
}
