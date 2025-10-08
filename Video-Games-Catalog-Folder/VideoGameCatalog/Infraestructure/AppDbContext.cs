using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.Models;

namespace VideoGameCatalog.Infraestructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<Game> Games { get; set; }
}