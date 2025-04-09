namespace Itmo.ObjectOrientedProgramming.Lab2.Users;

public interface IUser : IIdenticalEntity
{
    public new Guid Id { get; }

    public string Name { get; }
}
