using Microsoft.AspNetCore.Identity;

namespace WebAPI_Test.Interfaces;

public interface IPasswordHasher<TUser> where TUser : class
{
    public string Hash(TUser user, string password);

    public PasswordVerificationResult Verify(TUser user, string providedPassword);
}