using Microsoft.AspNetCore.Identity;
using WebAPI_Test.Models;

namespace WebAPI_Test.Tools;

public class UserPasswordHasher : Interfaces.IPasswordHasher<User>
{
    private PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

    public string Hash(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public PasswordVerificationResult Verify(User user, string providedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(user, user.Password, providedPassword);
    }
}