using Itmo.ObjectOrientedProgramming.Lab2;
using Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms;
using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.EditableSubjects;
using Itmo.ObjectOrientedProgramming.Lab2.Tasks;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using Xunit;
using Xunit.Abstractions;

namespace Lab2.Tests;

public class EducationProgramTests
{
    private const double MinCreditPointsDefault = 60;
    private readonly ITestOutputHelper _output;

    public EducationProgramTests(ITestOutputHelper testOutputHelper)
    {
        _output = testOutputHelper;
    }

    [Fact]
    public void UnauthorizedEditedLectureReturnsFailure()
    {
        // Arrange
        IUser lecturer = Lecturer
            .Build
            .WithUserName("Gosha")
            .Build();
        EditableByAuthorLecture lecture = EditableByAuthorLecture
            .Build
            .WithName("random lecture name")
            .WithAuthor(lecturer.Id)
            .WithDescription("random lecture description")
            .WithContent("GrAsP SoLiD BlA blA blah")
            .Build();
        IUser student = Student
            .Build
            .WithUserName("Andrey")
            .Build();

        // Act
        AuthorizedEditResult editResult = lecture.EditContent(student.Id, "bipki aboba pupkin");

        // Assert
        Assert.True(editResult is AuthorizedEditResult.Failure);
    }

    [Fact]
    public void UnauthorizedEditedTaskReturnsFailure()
    {
        // Arrange
        IUser lecturer = Lecturer.Build.WithUserName("Gosha").Build();
        EditableByAuthorTask labwork = LabWork
            .Build
            .WithName("labwork second")
            .WithAuthor(lecturer.Id)
            .WithPoints(12)
            .WithDescription("dummy university")
            .Build();
        IUser student = Student.Build.WithUserName("Andrey").Build();

        // Act
        AuthorizedEditResult editResult = labwork.EditDescription(student.Id, "finally interesting labwork");

        // Assert
        Assert.True(editResult is AuthorizedEditResult.Failure);
    }

    [Fact]
    public void UnauthorizedEditedExamSubjectReturnsFailure()
    {
        // Arrange
        const double examPoints = 100;
        IUser manager = Manager.Build.WithUserName("Alexander Vladimirovich").Build();
        SubjectCreateResult createResult = EditableByAuthorExamSubject
            .Build
            .WithName("OOP")
            .WithAuthor(manager.Id)
            .WithTasks([])
            .WithLectures([])
            .WithExamPoints(examPoints)
            .Build();
        IUser student = Student.Build.WithUserName("Andrey").Build();

        // Act
        switch (createResult)
        {
            case SubjectCreateResult.ExamSuccess successResult:
            {
                EditableByAuthorExamSubject subject = successResult.ExamSubject;
                AuthorizedEditResult editResult = subject.EditName(student.Id, "OPA PA");

                // Assert
                Assert.True(editResult is AuthorizedEditResult.Failure);
                break;
            }

            case SubjectCreateResult.Failure failureResult:
                _output.WriteLine(failureResult.Message);
                break;
            default:
                Assert.Fail();
                break;
        }
    }

    [Fact]
    public void UnauthorizedEditedCreditSubjectReturnsFailure()
    {
        // Arrange
        IUser manager = Manager.Build.WithUserName("Alexander Vladimirovich").Build();
        SubjectCreateResult createResult = EditableByAuthorCreditSubject
            .Build
            .WithName("Physics")
            .WithAuthor(manager.Id)
            .WithTasks([])
            .WithLectures([])
            .WithCreditPoints(MinCreditPointsDefault)
            .Build();
        IUser student = Student.Build.WithUserName("Andrey").Build();

        // Act
        switch (createResult)
        {
            case SubjectCreateResult.CreditSuccess successResult:
            {
                EditableByAuthorCreditSubject subject = successResult.CreditSubject;
                AuthorizedEditResult editResult = subject.EditName(student.Id, "F*ck physics");

                // Assert
                Assert.True(editResult is AuthorizedEditResult.Failure);
                break;
            }

            case SubjectCreateResult.Failure failureResult:
                _output.WriteLine(failureResult.Message);
                break;
            default:
                Assert.Fail();
                break;
        }
    }

    [Fact]
    public void ChildLectureIdIsEqualToFatherLectureId()
    {
        // Arrange
        IUser lecturer = Lecturer.Build.WithUserName("Gosha").Build();
        EditableByAuthorLecture lecture = EditableByAuthorLecture
            .Build
            .WithName("random lecture name")
            .WithAuthor(lecturer.Id)
            .WithDescription("random lecture description")
            .WithContent("GrAsP SoLiD BlA blA blah")
            .Build();

        // Act
        EditableByAuthorLecture nextYearLecture = lecture.Clone();

        // Assert
        Assert.Equal(lecture.Id, nextYearLecture.FatherId);
    }

    [Fact]
    public void ChildTaskIdIsEqualToFatherTaskId()
    {
        // Arrange
        IUser lecturer = Lecturer.Build.WithUserName("Gosha").Build();
        EditableByAuthorTask labwork = LabWork
            .Build
            .WithName("labwork second")
            .WithAuthor(lecturer.Id)
            .WithPoints(12)
            .WithDescription("dummy university")
            .Build();

        // Act
        EditableByAuthorTask nextYearLabwork = labwork.Clone();

        // Assert
        Assert.Equal(labwork.Id, nextYearLabwork.FatherId);
    }

    [Fact]
    public void ChildExamSubjectIdIsEqualToFatherId()
    {
        const double examPoints = 100;
        IUser manager = Manager.Build.WithUserName("Alexander Vladimirovich").Build();
        SubjectCreateResult createResult = EditableByAuthorExamSubject
            .Build
            .WithName("OOP")
            .WithAuthor(manager.Id)
            .WithTasks([])
            .WithLectures([])
            .WithExamPoints(examPoints)
            .Build();

        // Act
        switch (createResult)
        {
            case SubjectCreateResult.ExamSuccess successResult:
            {
                EditableByAuthorExamSubject subject = successResult.ExamSubject;
                EditableByAuthorExamSubject nextYearExamSubject = subject.Clone();

                // Assert
                Assert.Equal(subject.Id, nextYearExamSubject.FatherId);
                break;
            }

            case SubjectCreateResult.Failure failureResult:
                _output.WriteLine(failureResult.Message);
                break;
            default:
                Assert.Fail();
                break;
        }
    }

    [Fact]
    public void ChildCreditSubjectIdIsEqualToFatherId()
    {
        // Arrange
        IUser manager = Manager.Build.WithUserName("Alexander Vladimirovich").Build();
        SubjectCreateResult createResult = EditableByAuthorCreditSubject
            .Build
            .WithName("Physics")
            .WithAuthor(manager.Id)
            .WithTasks([])
            .WithLectures([])
            .WithCreditPoints(MinCreditPointsDefault)
            .Build();

        // Act
        switch (createResult)
        {
            case SubjectCreateResult.CreditSuccess successResult:
            {
                EditableByAuthorCreditSubject subject = successResult.CreditSubject;
                EditableByAuthorCreditSubject nextYearCreditSubject = subject.Clone();

                // Assert
                Assert.Equal(subject.Id, nextYearCreditSubject.FatherId);
                break;
            }

            case SubjectCreateResult.Failure failureResult:
                _output.WriteLine(failureResult.Message);
                break;
            default:
                Assert.Fail();
                break;
        }
    }

    [Fact]
    public void ChildEducationalProgramIdIsNotEqualToFatherId()
    {
        // Arrange
        IUser manager = Manager.Build.WithUserName("Alexander Vladimirovich").Build();
        IUser responsible = Manager.Build.WithUserName("Zubok").Build();

        // Act
        EducationalProgram educationalProgram = EducationalProgram
            .Build
            .WithName("IS")
            .WithManagerId(manager.Id)
            .WithResponsibleOfficialId(responsible.Id)
            .WithCreditSubjects(new Dictionary<SemesterNumber, EditableByAuthorCreditSubject>())
            .WithExamSubjects(new Dictionary<SemesterNumber, EditableByAuthorExamSubject>())
            .Build();
        EducationalProgram newEducationalProgram = educationalProgram.Clone();

        // Assert
        Assert.NotEqual(educationalProgram.Id, newEducationalProgram.Id);
    }

    [Fact]
    public void CreateExamSubjectWithPointsNotEqualTo100ReturnsFailure()
    {
        // Arrange & act
        const double examPoints = 80;
        IUser manager = Manager.Build.WithUserName("Alexander Vladimirovich").Build();
        IUser lecturer = Lecturer.Build.WithUserName("Gosha").Build();

        EditableByAuthorTask labwork = LabWork
            .Build
            .WithName("labwork second")
            .WithAuthor(lecturer.Id)
            .WithPoints(12)
            .WithDescription("dummy university")
            .Build();
        SubjectCreateResult createResult = EditableByAuthorExamSubject
            .Build
            .WithName("OOP")
            .WithAuthor(manager.Id)
            .WithTasks([labwork])
            .WithLectures([])
            .WithExamPoints(examPoints)
            .Build();

        // Assert
        Assert.True(createResult is SubjectCreateResult.Failure);
    }

    [Fact]
    public void CreateCreditSubjectWithPointsNotEqualTo100ReturnsFailure()
    {
        // Arrange & act
        const double minCreditPoints = 80;
        IUser manager = Manager
            .Build
            .WithUserName("Alexander Vladimirovich")
            .Build();
        IUser lecturer = Lecturer
            .Build.WithUserName("Gosha")
            .Build();
        EditableByAuthorTask labwork = LabWork
            .Build
            .WithName("labwork second")
            .WithAuthor(lecturer.Id)
            .WithPoints(20)
            .WithDescription("dummy university")
            .Build();
        EditableByAuthorTask labwork2 = LabWork
            .Build
            .WithName("labwork second")
            .WithAuthor(lecturer.Id)
            .WithPoints(12)
            .WithDescription("dummy university")
            .Build();

        SubjectCreateResult createResult = EditableByAuthorCreditSubject
            .Build
            .WithName("Physics")
            .WithAuthor(manager.Id)
            .WithTasks([labwork, labwork2])
            .WithLectures([])
            .WithCreditPoints(minCreditPoints)
            .Build();

        // Assert
        Assert.True(createResult is SubjectCreateResult.Failure);
    }

    [Fact]
    public void RepoTestFindSuccess()
    {
        // Arrange
        IUser manager = Manager
            .Build
            .WithUserName("Alexander Vladimirovich")
            .Build();
        IUser lecturer = Lecturer
            .Build.WithUserName("Gosha")
            .Build();

        var repo = new Repository<IUser>();

        // Act
        repo.AddEntity(lecturer);
        repo.AddEntity(manager);

        // Assert
        Assert.Equal("Alexander Vladimirovich", repo.GetEntity(manager.Id).Name);
    }

    [Fact]
    public void RepoTestFindFailure()
    {
        // Arrange
        IUser manager = Manager
            .Build
            .WithUserName("Alexander Vladimirovich")
            .Build();
        IUser lecturer = Lecturer
            .Build.WithUserName("Gosha")
            .Build();
        IUser student = Student.Build.WithUserName("Simon").Build();

        var repo = new Repository<IUser>();

        // Act
        repo.AddEntity(lecturer);
        repo.AddEntity(manager);

        Action act = () => repo.GetEntity(student.Id);

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal($"Entity with Id {student.Id} not found", exception.Message);
    }
}
