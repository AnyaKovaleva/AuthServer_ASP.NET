using Microsoft.AspNetCore.Mvc;
using WebAPI_Test.Login;

namespace WebAPI_Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LoginCommandHandler _loginCommandHandler;

    public AuthController(LoginCommandHandler loginCommandHandler)
    {
        _loginCommandHandler = loginCommandHandler;
    }

    [HttpPost]
    public async Task<IActionResult> LoginUser(
        [FromBody] LoginRequest loginRequest,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(loginRequest.email, loginRequest.password);
        var tokenResult = await _loginCommandHandler.Handle(command, cancellationToken);

        if (!tokenResult.IsOk)
        {
            if (tokenResult.StatusCode is NotFoundResult)
            {
                return NotFound();
            }

            if (tokenResult.StatusCode is UnauthorizedResult)
            {
                return Unauthorized();
            }

            return BadRequest();
        }

        return Ok(tokenResult.Token);
    }
}