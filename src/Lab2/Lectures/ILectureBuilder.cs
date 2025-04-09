namespace Itmo.ObjectOrientedProgramming.Lab2.Lectures;

public interface ILectureBuilder
{
    ILectureBuilder WithDescription(string description);

    ILectureBuilder WithContent(string content);

    EditableByAuthorLecture Build();
}

public interface ILectureNameSelector
{
    ILectureAuthorSelector WithName(string name);
}

public interface ILectureAuthorSelector
{
    ILectureBuilder WithAuthor(Guid authorId);
}
