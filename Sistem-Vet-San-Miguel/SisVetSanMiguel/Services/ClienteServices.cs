namespace SisVetSanMiguel.Services;
using SisVetSanMiguel.Models;

public class ClientServices
{
    private readonly AppDbContext _context;

    public ClientServices(AppDbContext context) 
    {
        _context = context;
    }

    public void createClient(string name, string document, string email, int idPet)
    {
        var client = new Client(name, document, email, idPet);
        _context.clients.Add(client);
    }
}