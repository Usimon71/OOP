using System.Security.Cryptography;
using System.Text;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Application.Admins;

public static class StringHash
{
    public static int GetStableHash(string input)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

        return BitConverter.ToInt32(bytes, 0);
    }
}
