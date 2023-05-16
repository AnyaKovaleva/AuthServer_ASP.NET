using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Test.Interfaces;
using WebAPI_Test.Models;

namespace WebAPI_Test.Login;

public class LoginCommandHandler
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly Interfaces.IPasswordHasher<User> _passwordHasher;

    public record LoginCommandResult
    {
        public string Token;
        public StatusCodeResult StatusCode;
        public bool IsOk;
    }

    public LoginCommandHandler(IUsersRepository usersRepository, IJwtProvider jwtProvider, Interfaces.IPasswordHasher<User> passwordHasher)
    {
        _usersRepository = usersRepository;
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //get user
        User? user = await _usersRepository.GetUserByEmail(request.email, cancellationToken);

        if (user is null)
        {
            return new LoginCommandResult()
            {
                Token = null,
                StatusCode = new NotFoundResult(),
                IsOk = false
            };
        }

        PasswordVerificationResult passwordVerificationResult = _passwordHasher.Verify(user, request.password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            return new LoginCommandResult()
            {
                Token = null,
                StatusCode = new UnauthorizedResult(),
                IsOk = false
            };
        }

        //Generate JWT
        string token = _jwtProvider.Generate(user);
        
        //Return it
        return new LoginCommandResult()
        {
            Token = token,
            StatusCode = new OkResult(),
            IsOk = true
        };
    }
}