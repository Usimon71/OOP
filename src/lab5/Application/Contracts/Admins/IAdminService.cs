namespace Application.Contracts.Admins;

public interface IAdminService
{
    AdminLoginResult Login(string password);
}
