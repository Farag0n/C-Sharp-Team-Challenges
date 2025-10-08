using Microsoft.EntityFrameworkCore;
using Game_Catalog.Models;

namespace Game_Catalog.Infraestructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 
    
    public DbSet<Game> Games { get; set; }
}