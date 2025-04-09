using Application.Abstractions.Repositories;
using Application.Contracts.Admins;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Application.Admins;

namespace Application.Application.Admins;

public class AdminService : IAdminService
{
    private readonly ISystemRepository _systemRepository;

    public AdminService(ISystemRepository systemRepository)
    {
        _systemRepository = systemRepository;
    }

    public AdminLoginResult Login(string password)
    {
        return StringHash.GetStableHash(password) != _systemRepository.GetPasswordHash()
            ? new AdminLoginResult.WrongPassword()
            : new AdminLoginResult.Success();
    }
}
