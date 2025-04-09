using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Tasks;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public interface IExamSubjectBuilder
{
    public SubjectCreateResult Build();
}

public interface ISubjectNameSelector
{
    ISubjectAuthorSelector WithName(string name);
}

public interface ISubjectAuthorSelector
{
    ISubjectTasksSelector WithAuthor(Guid authorId);
}

public interface ISubjectTasksSelector
{
    ISubjectLecturesSelector WithTasks(IList<EditableByAuthorTask> tasks);
}

public interface ISubjectLecturesSelector
{
    IExamSubjectPointsSelector WithLectures(IList<EditableByAuthorLecture> lectures);
}

public interface IExamSubjectPointsSelector
{
    IExamSubjectBuilder WithExamPoints(double examPoints);
}
