using SisVetSanMiguel.Domain.Models;
using SisVetSanMiguel.Domain.Interfaces;

namespace SisVetSanMiguel.Services;

public class AtentionServices : IGeneralCrud<Atention>
{
    public readonly AppDbContext _context;
    
    public AtentionServices(AppDbContext context)
    {
        _context = context;
    }

    // 🔹 Menú principal de atenciones
    public void AtentionMenu()
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
                    Console.Write("Ingrese el ID del cliente: ");
                    int clientId = int.Parse(Console.ReadLine());

                    Console.Write("Ingrese el ID de la mascota: ");
                    int petId = int.Parse(Console.ReadLine());

                    Console.Write("Ingrese el ID del veterinario: ");
                    int vetId = int.Parse(Console.ReadLine());

                    Console.Write("Ingrese la fecha (ej: 2025-09-28): ");
                    string date = Console.ReadLine();

                    Console.Write("Ingrese el reporte médico: ");
                    string report = Console.ReadLine();

                    var newAtention = new Atention
                    {
                        ClientId = clientId,
                        PetId = petId,
                        VetId = vetId,
                        Date = date,
                        MedicalReport = report
                    };

                    Add(newAtention);
                    Console.WriteLine("Atención registrada correctamente.");
                    Console.WriteLine("----------------------------------------------");
                    break;

                case "2":
                    GetAll();
                    break;

                case "3":
                    Console.WriteLine("----------------------------------------------");
                    Console.Write("Ingrese el ID de la atención que desea buscar: ");
                    int searchId = int.Parse(Console.ReadLine());
                    GetById(searchId);
                    break;

                case "4":
                    Console.WriteLine("----------------------------------------------");
                    Console.Write("Ingrese el ID de la atención que desea actualizar: ");
                    int updateId = int.Parse(Console.ReadLine());

                    Console.Write("Ingrese el nuevo ID del cliente: ");
                    int newClientId = int.Parse(Console.ReadLine());

                    Console.Write("Ingrese el nuevo ID de la mascota: ");
                    int newPetId = int.Parse(Console.ReadLine());

                    Console.Write("Ingrese el nuevo ID del veterinario: ");
                    int newVetId = int.Parse(Console.ReadLine());

                    Console.Write("Ingrese la nueva fecha: ");
                    string newDate = Console.ReadLine();

                    Console.Write("Ingrese el nuevo reporte médico: ");
                    string newReport = Console.ReadLine();

                    var updatedAtention = new Atention
                    {
                        ClientId = newClientId,
                        PetId = newPetId,
                        VetId = newVetId,
                        Date = newDate,
                        MedicalReport = newReport
                    };

                    Update(updatedAtention, updateId);
                    Console.WriteLine("Atención actualizada correctamente.");
                    Console.WriteLine("----------------------------------------------");
                    break;

                case "5":
                    Console.WriteLine("----------------------------------------------");
                    Console.Write("Ingrese el ID de la atención que desea eliminar: ");
                    int deleteId = int.Parse(Console.ReadLine());
                    Delete(deleteId);
                    Console.WriteLine("Atención eliminada correctamente.");
                    Console.WriteLine("----------------------------------------------");
                    break;

                case "6":
                    state = false;
                    break;

                default:
                    Console.WriteLine("Opción no válida, intente de nuevo.");
                    break;
            }
        }
    }

    // 🔹 Visualización del menú
    public void VisualMenu()
    {
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("==== Bienvenido al sub menú de Atenciones ====");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("=== Digite la opción que desea ===");
        Console.WriteLine("- 1 Registrar atención");
        Console.WriteLine("- 2 Ver todas las atenciones");
        Console.WriteLine("- 3 Buscar atención por ID");
        Console.WriteLine("- 4 Editar atención");
        Console.WriteLine("- 5 Eliminar atención por ID");
        Console.WriteLine("- 6 Volver al menú principal");
        Console.WriteLine("--------------------------------------------");
    }

    // 🔹 CRUD
    public void GetAll()
    {
        var atentions = _context.atentions.ToList();
        foreach (var atention in atentions)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"ID: {atention.Id}\nCliente: {atention.ClientId}\nMascota: {atention.PetId}\nVeterinario: {atention.VetId}\nFecha: {atention.Date}\nReporte: {atention.MedicalReport}");
            Console.WriteLine("----------------------------------------------");
        }
    }

    public void GetById(int id)
    {
        var atention = _context.atentions.Find(id);
        if (atention != null)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"ID: {atention.Id}\nCliente: {atention.ClientId}\nMascota: {atention.PetId}\nVeterinario: {atention.VetId}\nFecha: {atention.Date}\nReporte: {atention.MedicalReport}");
            Console.WriteLine("----------------------------------------------");
        }
        else
        {
            Console.WriteLine("La atención no fue encontrada.");
        }
    }

    public void Add(Atention entity)
    {
        _context.atentions.Add(entity);
        _context.SaveChanges();
    }

    public void Update(Atention entity, int id)
    {
        var existingAtention = _context.atentions.Find(id);
        if (existingAtention != null)
        {
            existingAtention.ClientId = entity.ClientId;
            existingAtention.PetId = entity.PetId;
            existingAtention.VetId = entity.VetId;
            existingAtention.Date = entity.Date;
            existingAtention.MedicalReport = entity.MedicalReport;

            _context.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var atention = _context.atentions.Find(id);
        if (atention != null)
        {
            _context.atentions.Remove(atention);
            _context.SaveChanges();
        }
    }
}
