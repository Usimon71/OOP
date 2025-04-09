namespace Itmo.ObjectOrientedProgramming.Lab2.Users.Builders;

public interface IUserBuilder
{
    IUser Build();
}

public interface IUserNameSelector
{
    IUserBuilder WithUserName(string name);
}
