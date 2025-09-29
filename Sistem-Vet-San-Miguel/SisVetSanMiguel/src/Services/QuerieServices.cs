using SisVetSanMiguel.Domain.Models;

namespace SisVetSanMiguel.Services;

public class QuerieService
{
    private readonly AppDbContext _context;

    public QuerieService(AppDbContext context)
    {
        _context = context;
    }

    // Todas las mascotas de un cliente
    public void GetPetsByClientId(int clientId)
    {
        var pets = _context.pets
            .Where(p => p.IdPetOwner == clientId)
            .ToList();

        if (pets.Any())
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Mascotas del cliente con ID {clientId}:");
            foreach (var pet in pets)
            {
                Console.WriteLine($"- {pet.Name} ({pet.Specie}, {pet.Race})");
            }
            Console.WriteLine("----------------------------------------------");
        }
        else
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Este cliente no tiene mascotas registradas.");
            Console.WriteLine("----------------------------------------------");
        }
    }

    // Veterinario con más atenciones realizadas
    public void GetVetWithMostAtentions()
    {
        var vet = _context.atentions
            .GroupBy(a => a.VetId)
            .Select(g => new
            {
                VetId = g.Key,
                TotalAtentions = g.Count()
            })
            .OrderByDescending(x => x.TotalAtentions)
            .FirstOrDefault();

        if (vet != null)
        {
            var vetInfo = _context.vets.Find(vet.VetId);
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Veterinario con más atenciones: {vetInfo.Name} ({vet.TotalAtentions} atenciones)");
            Console.WriteLine("----------------------------------------------");
        }
        else
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("No se encontraron atenciones registradas.");
            Console.WriteLine("----------------------------------------------");
        }
    }

    // Especie de mascota más atendida en la clínica
    public void GetMostAttendedSpecie()
    {
        var specie = _context.atentions
            .Join(_context.pets,
                  atention => atention.PetId,
                  pet => pet.Id,
                  (atention, pet) => pet.Specie)
            .GroupBy(s => s)
            .Select(g => new
            {
                Especie = g.Key,
                Total = g.Count()
            })
            .OrderByDescending(x => x.Total)
            .FirstOrDefault();

        if (specie != null)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"La especie más atendida es: {specie.Especie} ({specie.Total} atenciones)");
            Console.WriteLine("----------------------------------------------");
        }
        else
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("No hay registros de atenciones.");
            Console.WriteLine("----------------------------------------------");
        }
    }

    // Cliente con más mascotas registradas
    public void GetClientWithMostPets()
    {
        var client = _context.pets
            .GroupBy(p => p.IdPetOwner)
            .Select(g => new
            {
                ClientId = g.Key,
                TotalPets = g.Count()
            })
            .OrderByDescending(x => x.TotalPets)
            .FirstOrDefault();

        if (client != null)
        {
            var clientInfo = _context.clients.Find(client.ClientId);
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Cliente con más mascotas: {clientInfo.Name} ({client.TotalPets} mascotas registradas)");
            Console.WriteLine("----------------------------------------------");
        }
        else
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("No se encontraron clientes con mascotas.");
            Console.WriteLine("----------------------------------------------");
        }
    }

    // Mostrar historial médico de mascota
    public void GetMedicalHistoryByPet(int petId)
    {
        var history = _context.atentions
            .Where(a => a.PetId == petId)
            .OrderByDescending(a => a.Date) // ordena de la más reciente a la más antigua
            .ToList();

        if (history.Any())
        {
            Console.WriteLine($"Historial médico de la mascota con ID {petId}:");
            foreach (var atention in history)
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine($"Fecha: {atention.Date}");
                Console.WriteLine($"Veterinario ID: {atention.VetId}");
                Console.WriteLine($"Reporte médico: {atention.MedicalReport}");
            }
        }
        else
        {
            Console.WriteLine("No se encontraron atenciones para esta mascota.");
        }
    }

    // Método de bienvenida del menú
    //--------------------------------------------------------------------------------------------------
    public void VisualMenu()
    {
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("==== Bienvenido al sub menú de consultas avanzadas ====");
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("=== Digite la opción que desea ===");
        Console.WriteLine("- 1 Consultar todas las mascotas de un cliente");
        Console.WriteLine("- 2 Consultar el veterinario con más atenciones realizadas");
        Console.WriteLine("- 3 Consultar la especie de mascota más atendida");
        Console.WriteLine("- 4 Consultar el cliente con más mascotas registradas");
        Console.WriteLine("- 5 Consultar historial médico de una mascota");
        Console.WriteLine("- 6 Volver al menú principal");
        Console.WriteLine("----------------------------------------------");
    }
    //--------------------------------------------------------------------------------------------------

    // Menú principal de consultas
    //--------------------------------------------------------------------------------------------------
    public void AdvancedQueriesMenu()
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
                    GetPetsByClientId(clientId);
                    break;
                case "2":
                    GetVetWithMostAtentions();
                    break;
                case "3":
                    GetMostAttendedSpecie();
                    break;
                case "4":
                    GetClientWithMostPets();
                    break;
                case "5":
                    Console.Write("Ingrese el ID de la mascota: ");
                    int petId = int.Parse(Console.ReadLine());
                    GetMedicalHistoryByPet(petId);
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
}
