using Itmo.ObjectOrientedProgramming.Lab2.Subjects.EditableSubjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects.Builders;

public abstract record SubjectCreateResult
{
    public record ExamSuccess(EditableByAuthorExamSubject ExamSubject) : SubjectCreateResult;

    public record CreditSuccess(EditableByAuthorCreditSubject CreditSubject) : SubjectCreateResult;

    public record Failure(string Message) : SubjectCreateResult;
}
