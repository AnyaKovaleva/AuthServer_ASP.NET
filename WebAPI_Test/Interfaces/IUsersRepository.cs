using Microsoft.EntityFrameworkCore;
using WebAPI_Test.Models;

namespace WebAPI_Test.Interfaces;

public interface IUsersRepository
{
    public DbSet<User> Users { get; set; }
    public Task<User?> GetUserByEmail(string email,  CancellationToken cancellationToken);
}