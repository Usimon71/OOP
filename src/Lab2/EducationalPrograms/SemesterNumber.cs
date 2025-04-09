namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms;

public sealed record SemesterNumber : IEquatable<SemesterNumber>
{
    private const int MaxPossibleSemesters = 20;
    private readonly int _semesterNumber;

    public SemesterNumber(int semesterNumber)
    {
        if (_semesterNumber is < 1 or > MaxPossibleSemesters)
        {
            throw new ArgumentException($"The semester number must be between 1 and {MaxPossibleSemesters}");
        }

        _semesterNumber = semesterNumber;
    }

    public bool Equals(SemesterNumber? other)
    {
        if (other is null) return false;

        if (_semesterNumber != other._semesterNumber)
        {
            return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_semesterNumber);
    }
}
