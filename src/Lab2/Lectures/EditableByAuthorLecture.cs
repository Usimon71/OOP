namespace Itmo.ObjectOrientedProgramming.Lab2.Lectures;

public class EditableByAuthorLecture :
    IPrototype<EditableByAuthorLecture>,
    IIdenticalEntity
{
    private readonly Lecture _lecture;

    private bool AuthorIsNotTheSame(Guid authorId)
    {
        return _lecture.AuthorId != authorId;
    }

    public static ILectureNameSelector Build => new LectureBuilder();

    public Guid Id => _lecture.Id;

    public string Name => _lecture.Name;

    public Guid AuthorId => _lecture.AuthorId;

    public Guid? FatherId => _lecture.FatherId;

    public string Description => _lecture.Description;

    public string Content => _lecture.Content;

    public EditableByAuthorLecture(Lecture lecture)
    {
        _lecture = lecture;
    }

    public AuthorizedEditResult EditName(Guid authorId, string newName)
    {
        if (AuthorIsNotTheSame(authorId))
        {
            return new AuthorizedEditResult.Failure($"You are not permitted to edit this Lecture.");
        }

        _lecture.EditName(newName);

        return new AuthorizedEditResult.Success();
    }

    public AuthorizedEditResult EditDescription(Guid authorId, string newDescription)
    {
        if (AuthorIsNotTheSame(authorId))
        {
            return new AuthorizedEditResult.Failure($"You are not permitted to edit this Lecture.");
        }

        _lecture.EditDescription(newDescription);

        return new AuthorizedEditResult.Success();
    }

    public AuthorizedEditResult EditContent(Guid authorId, string newContent)
    {
        if (AuthorIsNotTheSame(authorId))
        {
            return new AuthorizedEditResult.Failure($"You are not permitted to edit this Lecture.");
        }

        _lecture.EditContent(newContent);

        return new AuthorizedEditResult.Success();
    }

    public EditableByAuthorLecture Clone()
    {
        return new EditableByAuthorLecture(_lecture.Clone());
    }

    public class LectureBuilder :
        ILectureBuilder,
        ILectureNameSelector,
        ILectureAuthorSelector
    {
        private readonly Guid _noFatherId = Guid.Empty;
        private string? _name;
        private Guid _authorId;
        private string? _description;
        private string? _content;

        public ILectureAuthorSelector WithName(string name)
        {
            _name = name;

            return this;
        }

        public ILectureBuilder WithAuthor(Guid authorId)
        {
            _authorId = authorId;

            return this;
        }

        public ILectureBuilder WithDescription(string description)
        {
            _description = description;

            return this;
        }

        public ILectureBuilder WithContent(string content)
        {
            _content = content;

            return this;
        }

        public EditableByAuthorLecture Build()
        {
            return new EditableByAuthorLecture(
                new Lecture(
                    _name ?? throw new ArgumentNullException(),
                    _authorId,
                    _description,
                    _content,
                    _noFatherId));
        }
    }
}
