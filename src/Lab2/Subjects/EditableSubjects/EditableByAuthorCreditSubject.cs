using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.Subject;
using Itmo.ObjectOrientedProgramming.Lab2.Tasks;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects.EditableSubjects;

public class EditableByAuthorCreditSubject :
    IIdenticalEntity,
    IPrototype<EditableByAuthorCreditSubject>
{
    private const double MinPossibleCreditPoints = 60;
    private const double MaxPossibleCreditPoints = 100;
    private readonly CreditSubject _creditSubject;

    private bool AuthorIsNotTheSame(Guid authorId)
    {
        return _creditSubject.AuthorId != authorId;
    }

    public static Builder Build => new();

    public Guid Id => _creditSubject.Id;

    public string Name => _creditSubject.Name;

    public Guid AuthorId => _creditSubject.AuthorId;

    public Guid? FatherId => _creditSubject.FatherId;

    public IList<EditableByAuthorTask> Tasks => _creditSubject.Tasks;

    public IList<EditableByAuthorLecture> Lectures => _creditSubject.Lectures;

    public double MinCreditPoints => _creditSubject.MinCreditPoints;

    private EditableByAuthorCreditSubject(CreditSubject creditSubject)
    {
        _creditSubject = creditSubject;
    }

    public AuthorizedEditResult EditName(Guid authorId, string newName)
    {
        if (AuthorIsNotTheSame(authorId))
        {
            return new AuthorizedEditResult.Failure("You are not permitted to edit this subject.");
        }

        _creditSubject.EditName(newName);

        return new AuthorizedEditResult.Success();
    }

    public AuthorizedEditResult EditMinCreditPoints(Guid authorId, double newMinCreditPoints)
    {
        if (AuthorIsNotTheSame(authorId))
        {
            return new AuthorizedEditResult.Failure("You are not permitted to edit this subject.");
        }

        switch (newMinCreditPoints)
        {
            case < MinPossibleCreditPoints:
                return new AuthorizedEditResult.Failure("Credit min points need to be at least 60.");
            case > MaxPossibleCreditPoints:
                return new AuthorizedEditResult.Failure("Credit min points need to be at most 100.");
            default:
                _creditSubject.EditMinCreditPoints(newMinCreditPoints);

                return new AuthorizedEditResult.Success();
        }
    }

    public EditableByAuthorCreditSubject Clone()
    {
        var tasks = _creditSubject.Tasks.Select(task => task.Clone()).ToList();
        var lectures = _creditSubject.Lectures.Select(lecture => lecture.Clone()).ToList();
        return new EditableByAuthorCreditSubject(new CreditSubject(
            Name,
            AuthorId,
            Id,
            tasks,
            lectures,
            MinCreditPoints));
    }

    public class Builder :
    ICreditSubjectNameSelector,
    ICreditSubjectAuthorSelector,
    ICreditSubjectTasksSelector,
    ICreditSubjectLecturesSelector,
    ICreditSubjectBuilder,
    ICreditSubjectPointsSelector
{
    private const double MinPossibleCreditPoints = 60;
    private const double MaxPossibleCreditPoints = 100;
    private readonly Guid _noFatherId = Guid.Empty;
    private string _name = string.Empty;
    private Guid _authorId;
    private IList<EditableByAuthorTask> _tasks = [];
    private IList<EditableByAuthorLecture> _lectures = [];
    private double _minCreditPoints;

    public ICreditSubjectAuthorSelector WithName(string name)
    {
        _name = name;

        return this;
    }

    public ICreditSubjectTasksSelector WithAuthor(Guid authorId)
    {
        _authorId = authorId;

        return this;
    }

    public ICreditSubjectLecturesSelector WithTasks(IList<EditableByAuthorTask> tasks)
    {
        _tasks = tasks;

        return this;
    }

    public ICreditSubjectPointsSelector WithLectures(IList<EditableByAuthorLecture> lectures)
    {
        _lectures = lectures;

        return this;
    }

    public Builder WithMinCreditPoints(double minCreditPoints)
    {
        _minCreditPoints = minCreditPoints;

        return this;
    }

    public ICreditSubjectBuilder WithCreditPoints(double minCreditPoints)
    {
        _minCreditPoints = minCreditPoints;

        return this;
    }

    public SubjectCreateResult Build()
    {
        double sum = _tasks.Sum(task => task.Points);
        if (Math.Abs(sum - 100) > 0.001)
        {
            return new SubjectCreateResult.Failure("Subject points cannot be more than 100.");
        }

        switch (_minCreditPoints)
        {
            case < MinPossibleCreditPoints:
                return new SubjectCreateResult.Failure("Min credit points must be at least 60.");
            case > MaxPossibleCreditPoints:
                return new SubjectCreateResult.Failure("Min credit points must be at most 100.");
            default:
            {
                var editableCreditSubject =
                    new EditableByAuthorCreditSubject(new CreditSubject(
                        _name,
                        _authorId,
                        _noFatherId,
                        _tasks,
                        _lectures,
                        _minCreditPoints));

                return new SubjectCreateResult.CreditSuccess(editableCreditSubject);
            }
        }
    }
}
}
