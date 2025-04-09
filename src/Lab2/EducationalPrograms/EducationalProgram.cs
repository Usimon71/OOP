using Itmo.ObjectOrientedProgramming.Lab2.Subjects.EditableSubjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms;

public class EducationalProgram :
    IIdenticalEntity,
    IPrototype<EducationalProgram>
{
    public static Builder Build => new();

    public Guid Id { get; }

    public string Name { get; }

    public Guid ResponsibleOfficialId { get; }

    public Guid ManagerId { get; }

    public IDictionary<SemesterNumber, EditableByAuthorExamSubject> SemesterToExamSubjects { get; }

    public IDictionary<SemesterNumber, EditableByAuthorCreditSubject> SemesterToCreditSubjects { get; }

    public EducationalProgram Clone()
    {
        Dictionary<SemesterNumber, EditableByAuthorExamSubject> semesterToExamSubjects = [];
        foreach (KeyValuePair<SemesterNumber, EditableByAuthorExamSubject> entry in SemesterToExamSubjects)
        {
            semesterToExamSubjects.Add(entry.Key, entry.Value.Clone());
        }

        Dictionary<SemesterNumber, EditableByAuthorCreditSubject> semesterToCreditSubjects = [];
        foreach (KeyValuePair<SemesterNumber, EditableByAuthorCreditSubject> entry in SemesterToCreditSubjects)
        {
            semesterToCreditSubjects.Add(entry.Key, entry.Value.Clone());
        }

        return new EducationalProgram(
            Name,
            ResponsibleOfficialId,
            ManagerId,
            semesterToExamSubjects,
            semesterToCreditSubjects);
    }

    private EducationalProgram(
        string name,
        Guid responsibleOfficialId,
        Guid managerId,
        IDictionary<SemesterNumber, EditableByAuthorExamSubject> semesterToExamSubjects,
        IDictionary<SemesterNumber, EditableByAuthorCreditSubject> semesterToCreditSubjects)
    {
        Id = Guid.NewGuid();
        Name = name;
        ResponsibleOfficialId = responsibleOfficialId;
        ManagerId = managerId;
        SemesterToExamSubjects = semesterToExamSubjects;
        SemesterToCreditSubjects = semesterToCreditSubjects;
    }

    public class Builder
    {
        private string? _name;

        private Guid _responsibleOfficialId;

        private Guid _managerId;

        private IDictionary<SemesterNumber, EditableByAuthorExamSubject>? _semesterToExamSubjects;

        private IDictionary<SemesterNumber, EditableByAuthorCreditSubject>? _semesterToCreditSubjects;

        public Builder WithName(string name)
        {
            _name = name;

            return this;
        }

        public Builder WithResponsibleOfficialId(Guid responsibleOfficialId)
        {
            _responsibleOfficialId = responsibleOfficialId;

            return this;
        }

        public Builder WithManagerId(Guid managerId)
        {
            _managerId = managerId;

            return this;
        }

        public Builder WithExamSubjects(IDictionary<SemesterNumber, EditableByAuthorExamSubject> semesterToExamSubjects)
        {
            _semesterToExamSubjects = semesterToExamSubjects;

            return this;
        }

        public Builder WithCreditSubjects(IDictionary<SemesterNumber, EditableByAuthorCreditSubject> semesterToCreditSubjects)
        {
            _semesterToCreditSubjects = semesterToCreditSubjects;

            return this;
        }

        public EducationalProgram Build()
        {
            return new EducationalProgram(
                _name ?? throw new ArgumentNullException(nameof(_name)),
                _responsibleOfficialId,
                _managerId,
                _semesterToExamSubjects ?? throw new ArgumentNullException(nameof(_name)),
                _semesterToCreditSubjects ?? throw new ArgumentNullException(nameof(_name)));
        }
    }
}
