using SisVetSanMiguel.Domain.Interfaces;

namespace SisVetSanMiguel.Services;

public class ClientServices : IGeneralCrud<Client>
{
    public readonly AppDbContext _context;
    
    // Constructor
    public ClientServices(AppDbContext context)
    {
        _context = context;
    }

    // Obtener todos los clientes
    public void GetAll()
    {
        var clients = _context.clients.ToList();
        foreach (var client in clients)
        {
            Console.WriteLine($"Name: {client.Name}, Document: {client.Document}, Email: {client.Email}");
        }
    }

    public void GetById(int id)
    {
        var client = _context.clients.Find(id);
        if (client != null)
        {
            Console.WriteLine($"Nombre: {client.Name}, Documento: {client.Document}, Email: {client.Document}");
        }
        else
        {
            Console.WriteLine("Cliente no encontrado.");
        }
    }      
    
    // Crear clientes 
    public void Add(Client entity)
    {
        _context.clients.Add(entity);
        _context.SaveChanges();
    }        

    // Actualizar cliente
    public void Update(Client entity, int id)
    {
        var clienteExistente = _context.clients.Find(id);
        if (clienteExistente != null)
        {
            clienteExistente.Name = entity.Name;
            clienteExistente.Document = entity.Document;
            clienteExistente.Email = entity.Email;

            _context.SaveChanges();
        }
    }


    // Eliminar cliente
    public void Delete(int id)
    {
        var client = _context.clients.Find(id);
        if (client != null)
        {
            _context.clients.Remove(client);
            _context.SaveChanges();
        }
    }
}
