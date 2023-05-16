using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Test.Data;
using WebAPI_Test.Interfaces;
using WebAPI_Test.Login;
using WebAPI_Test.Models;

namespace WebAPI_Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly SecretDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UsersController(SecretDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<User>> Get()
    {
        return await _context.Users.ToListAsync();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(User user)
    {
        string hashedPassword = _passwordHasher.Hash(user, user.Password);
        user.Password = hashedPassword;
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

 
}