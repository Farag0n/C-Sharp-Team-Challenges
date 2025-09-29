namespace SisVetSanMiguel.Services;

using SisVetSanMiguel.Domain.Interfaces;
using SisVetSanMiguel.Domain.Models;


public class VetService : IGeneralCrud<Vet>
{
    private readonly AppDbContext _context;

    public VetService(AppDbContext context)
    {
        _context = context;
    }

    //Metodo para ver todos los veterinarios
    //--------------------------------------------------------------------------------------------------
    public void GetAll()
    {
        var vets = _context.vets.ToList();

        foreach (var vet in vets)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine(
                $"ID: {vet.Id} Nombre: {vet.Name}\nDocument: {vet.Document}\nEmail: {vet.Email}\nAtenciones: {vet.Atentions}");
            Console.WriteLine("------------------------------------");
        }
    }
    //--------------------------------------------------------------------------------------------------

    //Metodo para buscar veterinario por ID (ahora devuelve Vet?)
    //--------------------------------------------------------------------------------------------------
    public Vet? GetById(int id)
    {
        return _context.vets.Find(id);
    }
    //--------------------------------------------------------------------------------------------------

    //Metodo para agregar veterinarios
    //--------------------------------------------------------------------------------------------------
    public void Add(Vet entity)
    {
        _context.vets.Add(entity);
        _context.SaveChanges();
    }
    //--------------------------------------------------------------------------------------------------

    //Metodo para actualizar/editar veterinario (simplificado, solo recibe entity)
    //--------------------------------------------------------------------------------------------------
    public void Update(Vet entity)
    {
        var existingVet = _context.vets.Find(entity.Id);

        if (existingVet != null)
        {
            existingVet.Name = entity.Name;
            existingVet.Document = entity.Document;
            existingVet.Email = entity.Email;
            existingVet.Atentions = entity.Atentions;

            _context.SaveChanges();
        }
    }
    //--------------------------------------------------------------------------------------------------


    //Metodo para eliminar veterinario
    //--------------------------------------------------------------------------------------------------
    public void Delete(int id)
    {
        var vet = _context.vets.Find(id);
        if (vet != null)
        {
            _context.vets.Remove(vet);
            _context.SaveChanges();
        }
    }
    //--------------------------------------------------------------------------------------------------


    //Metodo para agregar desde la terminal
    //--------------------------------------------------------------------------------------------------
    public void AgregarVeterinario()
    {
        Console.WriteLine("----------------------------------------");
        Console.Write("Ingresa el nombre de la persona: ");
        string name = Console.ReadLine();

        Console.Write("Ingresa el documento: ");
        string document = Console.ReadLine();

        Console.Write("Ingresa el email: ");
        string email = Console.ReadLine();

        Console.Write("Ingresa el numero de atenciones: ");
        int attentions = int.Parse(Console.ReadLine());

        var vets = new Vet
        {
            Name = name,
            Document = document,
            Email = email,
            Atentions = attentions
        };

        Add(vets);

        Console.WriteLine("Veterinario agregado con exito!");
        Console.WriteLine("----------------------------------------");
    }
    //--------------------------------------------------------------------------------------------------



    //Metodo para pedir los datos en terminal del la busqueda por id
    //--------------------------------------------------------------------------------------------------
    public void BuscarVetPorID()
    {
        Console.WriteLine("-------------------------------------------");
        Console.Write("Ingrese el ID el veterinario que desea buscar: ");
        int searchVet = int.Parse(Console.ReadLine());

        var vet = GetById(searchVet);
        if (vet != null)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine(
                $"Nombre: {vet.Name}\nDocument: {vet.Document}\nEmail: {vet.Email}\nAtenciones: {vet.Atentions}");
            Console.WriteLine("------------------------------------");
        }
        else
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("La persona no fue encontrada");
            Console.WriteLine("----------------------------");
        }
    }
    //--------------------------------------------------------------------------------------------------


    public void EditarVeterinario()
    {
        Console.WriteLine("----------------------------------------------");
        Console.Write("Ingrese el ID de la persona que desea actualizar: ");
        int IdEditar = int.Parse(Console.ReadLine());

        Vet? vetEditar = GetById(IdEditar);
        if (vetEditar != null)
        {
            Console.Write("Ingrese el nuevo nombre: ");
            vetEditar.Name = Console.ReadLine();

            Console.Write("Ingrese el documento nuevo: ");
            vetEditar.Document = Console.ReadLine();

            Console.Write("Ingrese el email nuevo: ");
            vetEditar.Email = Console.ReadLine();

            Console.Write("Ingresa las atenciones nuevas: ");
            vetEditar.Atentions = int.Parse(Console.ReadLine());

            Update(vetEditar);

            Console.WriteLine("✅ Veterinario actualizado.");
        }
        else
        {
            Console.WriteLine("Veterinario no encontrado.");
        }
    }


    public void EliminarVeterinario()
    {
        Console.Write("Ingresa el ID el veterinario que deseas eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int IdEliminar))
        {
            Console.WriteLine("Opcion no valida");
            return;
        }

        var vet = GetById(IdEliminar);
        if (vet != null)
        {
            Delete(IdEliminar);
            Console.WriteLine("Veterinario eliminado con exito");
        }
        else
        {
            Console.WriteLine("Ingresa un ID valido");
        }
    }

    // Método auxiliar para que el menú compile
    public void ListarVeterinarios()
    {
        GetAll();
    }


    public void Menu()
    {
        bool state = true;

        while (state)
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("==== Bienvenido a el sub menu de veterinario ===");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("\n--- Gestion de Veterinarios ---");
            Console.WriteLine("- 1. Agrega un nuevo veterinario.");
            Console.WriteLine("- 2. Listar los veterinarios");
            Console.WriteLine("- 3. Busca un veterinario por su id.");
            Console.WriteLine("- 4. Edita un veterinario.");
            Console.WriteLine("- 5. Elimina un veterinario");
            Console.WriteLine("- 6. Salir del menu.");
            Console.WriteLine("--------------------------------------------");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AgregarVeterinario();
                    break;
                case "2":
                    ListarVeterinarios();
                    break;
                case "3":
                    BuscarVetPorID();
                    break;
                case "4":
                    EditarVeterinario();
                    break;
                case "5":
                    EliminarVeterinario();
                    break;
                case "6":
                    Console.WriteLine("Saliendo del menu");
                    state = false;
                    break;
                default:
                    Console.WriteLine("Opcion Invalida");
                    break;
            }
        }


    }
}