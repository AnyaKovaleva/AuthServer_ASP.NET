using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Test.Data;
using WebAPI_Test.Models;

namespace WebAPI_Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SecretsController : ControllerBase
{
    private readonly SecretDbContext _context;

    public SecretsController(SecretDbContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<Secret>> Get()
    {
        return await _context.Secrets.ToListAsync();
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Secret), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var secret = await _context.Secrets.FindAsync(id);
        return secret == null ? NotFound() : Ok(secret);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(Secret secret)
    { 
        await _context.Secrets.AddAsync(secret);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = secret.Id }, secret);
    }
}