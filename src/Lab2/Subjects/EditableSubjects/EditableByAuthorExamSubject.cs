using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.Subject;
using Itmo.ObjectOrientedProgramming.Lab2.Tasks;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects.EditableSubjects;

public class EditableByAuthorExamSubject :
    IIdenticalEntity,
    IPrototype<EditableByAuthorExamSubject>
{
    private readonly ExamSubject _examSubject;

    private bool AuthorIsNotTheSame(Guid authorId)
    {
        return _examSubject.AuthorId != authorId;
    }

    public static Builder Build => new();

    public Guid Id => _examSubject.Id;

    public string Name => _examSubject.Name;

    public Guid AuthorId => _examSubject.AuthorId;

    public Guid? FatherId => _examSubject.FatherId;

    public IList<EditableByAuthorTask> Tasks => _examSubject.Tasks;

    public IList<EditableByAuthorLecture> Lectures => _examSubject.Lectures;

    public double ExamPoints => _examSubject.ExamPoints;

    public EditableByAuthorExamSubject(ExamSubject examSubject)
    {
        _examSubject = examSubject;
    }

    public AuthorizedEditResult EditName(Guid authorId, string newName)
    {
        if (AuthorIsNotTheSame(authorId))
        {
            return new AuthorizedEditResult.Failure("You are not permitted to edit this subject.");
        }

        _examSubject.EditName(newName);

        return new AuthorizedEditResult.Success();
    }

    public EditableByAuthorExamSubject Clone()
    {
        var tasks = _examSubject.Tasks.Select(task => task.Clone()).ToList();
        var lectures = _examSubject.Lectures.Select(lecture => lecture.Clone()).ToList();
        return new EditableByAuthorExamSubject(new ExamSubject(
            Name,
            AuthorId,
            Id,
            tasks,
            lectures,
            ExamPoints));
    }

    public class Builder :
        ISubjectNameSelector,
        ISubjectAuthorSelector,
        ISubjectTasksSelector,
        ISubjectLecturesSelector,
        IExamSubjectBuilder,
        IExamSubjectPointsSelector
    {
        private readonly Guid _noFatherId = Guid.Empty;
        private string _name = string.Empty;
        private Guid _authorId;
        private IList<EditableByAuthorTask> _tasks = [];
        private IList<EditableByAuthorLecture> _lectures = [];
        private double _examPoints;

        public ISubjectAuthorSelector WithName(string name)
        {
            _name = name;

            return this;
        }

        public ISubjectTasksSelector WithAuthor(Guid authorId)
        {
            _authorId = authorId;

            return this;
        }

        public ISubjectLecturesSelector WithTasks(IList<EditableByAuthorTask> tasks)
        {
            _tasks = tasks;

            return this;
        }

        public IExamSubjectPointsSelector WithLectures(IList<EditableByAuthorLecture> lectures)
        {
            _lectures = lectures;

            return this;
        }

        public IExamSubjectBuilder WithExamPoints(double examPoints)
        {
            _examPoints = examPoints;

            return this;
        }

        public SubjectCreateResult Build()
        {
            double sum = _tasks.Sum(task => task.Points);
            if (Math.Abs(sum - 100) + _examPoints > 0.001)
            {
                return new SubjectCreateResult.Failure("Subject points cannot be more than 100.");
            }

            var editableExamSubject = new EditableByAuthorExamSubject(new ExamSubject(
                _name,
                _authorId,
                _noFatherId,
                _tasks,
                _lectures,
                _examPoints));

            return new SubjectCreateResult.ExamSuccess(editableExamSubject);
        }
    }
}
