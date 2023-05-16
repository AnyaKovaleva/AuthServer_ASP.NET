using Microsoft.EntityFrameworkCore;
using WebAPI_Test.Interfaces;
using WebAPI_Test.Models;

namespace WebAPI_Test.Data;

public class SecretDbContext : DbContext, IUsersRepository
{
    public SecretDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Secret> Secrets { get; set; }

    public DbSet<User> Users { get; set; }
    public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        return await Users.Where(v => v.Email == email).FirstOrDefaultAsync(cancellationToken);
    }
}