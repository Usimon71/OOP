namespace Application.Contracts.Admins;

public record AdminLoginResult
{
    public sealed record Success : AdminLoginResult;

    public sealed record WrongPassword : AdminLoginResult;
}
