using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Tasks;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public interface ICreditSubjectBuilder
{
    public SubjectCreateResult Build();
}

public interface ICreditSubjectPointsSelector
{
    ICreditSubjectBuilder WithCreditPoints(double minCreditPoints);
}

public interface ICreditSubjectNameSelector
{
    ICreditSubjectAuthorSelector WithName(string name);
}

public interface ICreditSubjectAuthorSelector
{
    ICreditSubjectTasksSelector WithAuthor(Guid authorId);
}

public interface ICreditSubjectTasksSelector
{
    ICreditSubjectLecturesSelector WithTasks(IList<EditableByAuthorTask> tasks);
}

public interface ICreditSubjectLecturesSelector
{
    ICreditSubjectPointsSelector WithLectures(IList<EditableByAuthorLecture> lectures);
}
