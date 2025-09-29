using SisVetSanMiguel.Domain.Interfaces;
using SisVetSanMiguel.Domain.Models;

namespace SisVetSanMiguel.Services;

public class ClientServices : IGeneralCrud<Client>
{
    public readonly AppDbContext _context;
    
    // Constructor
    public ClientServices(AppDbContext context)
    {
        _context = context;
    }

    // Método del menú
    //--------------------------------------------------------------------------------------------------
    public void ClientMenu()
    {
        bool state = true;

        while (state)
        {
            VisualMenu();

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine("----------------------------------------------");
                    Console.Write("Ingrese el nombre del cliente: ");
                    string name = Console.ReadLine();

                    Console.Write("Ingrese el documento: ");
                    string document = Console.ReadLine();

                    Console.Write("Ingrese el email: ");
                    string email = Console.ReadLine();

                    var newClient = new Client
                    {
                        Name = name,
                        Document = document,
                        Email = email
                    };

                    Add(newClient);
                    Console.WriteLine("Cliente registrado correctamente.");
                    Console.WriteLine("----------------------------------------------");
                    break;

                case "2":
                    GetAll();
                    break;

                case "3":
                    Console.WriteLine("----------------------------------------------");
                    Console.Write("Ingrese el ID del cliente a buscar: ");
                    int searchId = int.Parse(Console.ReadLine());
                    GetById(searchId);
                    Console.WriteLine("----------------------------------------------");
                    break;

                case "4":
                    Console.WriteLine("----------------------------------------------");
                    Console.Write("Ingrese el ID del cliente a actualizar: ");
                    int updateId = int.Parse(Console.ReadLine());

                    Console.Write("Ingrese el nuevo nombre: ");
                    string newName = Console.ReadLine();

                    Console.Write("Ingrese el nuevo documento: ");
                    string newDocument = Console.ReadLine();

                    Console.Write("Ingrese el nuevo email: ");
                    string newEmail = Console.ReadLine();

                    var updatedClient = new Client
                    {
                        Name = newName,
                        Document = newDocument,
                        Email = newEmail
                    };

                    Update(updatedClient, updateId);
                    Console.WriteLine("Cliente actualizado correctamente.");
                    Console.WriteLine("----------------------------------------------");
                    break;

                case "5":
                    Console.WriteLine("----------------------------------------------");
                    Console.Write("Ingrese el ID del cliente a eliminar: ");
                    int deleteId = int.Parse(Console.ReadLine());

                    Delete(deleteId);
                    Console.WriteLine("Cliente eliminado correctamente.");
                    Console.WriteLine("----------------------------------------------");
                    break;

                case "6":
                    state = false;
                    break;

                default:
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine("La opción no es válida, intente de nuevo.");
                    Console.WriteLine("----------------------------------------------");
                    break;
            }
        }
    }
    //--------------------------------------------------------------------------------------------------

    // Método de bienvenida de menú
    //--------------------------------------------------------------------------------------------------
    public void VisualMenu()
    {
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("==== Bienvenido al sub menú de clientes ====");
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("=== Digite la opción que desea ===");
        Console.WriteLine("- 1 Registrar cliente");
        Console.WriteLine("- 2 Ver clientes registrados");
        Console.WriteLine("- 3 Buscar cliente por ID");
        Console.WriteLine("- 4 Editar cliente");
        Console.WriteLine("- 5 Eliminar cliente por ID");
        Console.WriteLine("- 6 Volver al menú principal");
        Console.WriteLine("----------------------------------------------");
    }
    //--------------------------------------------------------------------------------------------------

    // Obtener todos los clientes
    public void GetAll()
    {
        var clients = _context.clients.ToList();
        foreach (var client in clients)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Nombre: {client.Name}\nDocumento: {client.Document}\nEmail: {client.Email}");
            Console.WriteLine("----------------------------------------------");
        }
    }

    // Buscar cliente por ID
    public void GetById(int id)
    {
        var client = _context.clients.Find(id);
        if (client != null)
        {
            Console.WriteLine($"Nombre: {client.Name}\nDocumento: {client.Document}\nEmail: {client.Email}");
        }
        else
        {
            Console.WriteLine("Cliente no encontrado.");
        }
    }      
    
    // Crear cliente 
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
