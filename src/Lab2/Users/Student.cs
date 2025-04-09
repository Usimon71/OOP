using Itmo.ObjectOrientedProgramming.Lab2.Users.Builders;

namespace Itmo.ObjectOrientedProgramming.Lab2.Users;

public record Student : IUser
{
    public Guid Id { get; }

    public string Name { get; }

    public Student(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
    }

    public static Builder Build => new();

    public class Builder :
        IUserBuilder,
        IUserNameSelector
    {
        private string? _name;

        public IUserBuilder WithUserName(string name)
        {
            _name = name;

            return this;
        }

        public IUser Build()
        {
            return new Student(
                _name ?? throw new ArgumentNullException(nameof(_name)));
        }
    }
}
