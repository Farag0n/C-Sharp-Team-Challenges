using Microsoft.EntityFrameworkCore;
using SisVetSanMiguel.Domain.Models;

namespace SisVetSanMiguel;

public class AppDbContext : DbContext
{
    public DbSet<Client> clients { get; set; }
    public DbSet<Vet> vets { get; set; }
    public DbSet<Atention> atentions { get; set; }
    public DbSet<Pet> pets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseMySql(
                connectionString: "server=localhost; user=root;database=riw_test2; password=20010928",
                
                MySqlServerVersion.AutoDetect("server=localhost; user=root;database=riw_test2; password=20010928")
                
            );
        }
    }
    
}