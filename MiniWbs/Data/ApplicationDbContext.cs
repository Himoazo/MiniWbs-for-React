using Microsoft.EntityFrameworkCore;
using MiniWbs.Models;

namespace MiniWbs.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }
}
