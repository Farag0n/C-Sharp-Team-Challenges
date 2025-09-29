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
            Console.WriteLine($"Mascotas del cliente con id {clientId}:");
            foreach (var pet in pets)
            {
                Console.WriteLine($"- {pet.Name} ({pet.Specie}, {pet.Race})");
            }
        }
        else
        {
            Console.WriteLine("Este cliente no tiene mascotas registradas.");
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
            Console.WriteLine($"Veterinario con mas atenciones: {vetInfo.Name} ({vet.TotalAtentions} atenciones)");
        }
        else
        {
            Console.WriteLine("No se encontraron atenciones registradas.");
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
            Console.WriteLine($"La especie mas atendida es: {specie.Especie} ({specie.Total} atenciones)");
        }
        else
        {
            Console.WriteLine("No hay registros de atenciones.");
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
            Console.WriteLine($"Cliente con mas mascotas: {clientInfo.Name} ({client.TotalPets} mascotas registradas)");
        }
        else
        {
            Console.WriteLine("No se encontraron clientes con mascotas.");
        }
    }

    // Menu de consultas
    public void AdvancedQueriesMenu()
    {
        bool state = true;
        while (state)
        {
            Console.WriteLine("\n==== Consultas Avanzadas ====");
            Console.WriteLine("1. Consultar todas las mascotas de un cliente");
            Console.WriteLine("2. Consultar el veterinario con mas atenciones realizadas");
            Console.WriteLine("3. Consultar la especie de mascota mas atendida");
            Console.WriteLine("4. Consultar el cliente con mas mascotas registradas");
            Console.WriteLine("5. Salir");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
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
                    state = false;
                    break;
                default:
                    Console.WriteLine("Esta opcion no es valida");
                    break;
            }
        }
    }
}
