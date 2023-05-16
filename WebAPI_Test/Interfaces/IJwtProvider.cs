using WebAPI_Test.Models;

namespace WebAPI_Test.Interfaces;

public interface IJwtProvider
{
    public string Generate(User user);
}