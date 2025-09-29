using System;
using System.Collections.Generic;
using System.Linq;
using SisVetSanMiguel.Domain.Interfaces;
using SisVetSanMiguel.Domain.Models;


namespace SisVetSanMiguel.Services;

public class VeterinariaService : IGeneralCrud<Vet>
{
    private readonly AppDbContext _context;

    public VeterinariaService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Vet> GetAll()
    {
        return _context.vets.ToList();
    }

    public Vet? GetById(int id)
    {
        return _context.vets.Find(id);
    }

    public void Add(Vet vet)
    {
        _context.vets.Add(vet);
        _context.SaveChanges();
    }

    public void Update(Vet vet)
    {
        var existente = _context.vets.Find(vet.Id);
        if (existente != null)
        {
            existente.Name = vet.Name;
            existente.Document = vet.Document;
            existente.Email = vet.Email;
            existente.Atentions = vet.Atentions;

            _context.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var vet = _context.vets.Find(id);
        if (vet != null)
        {
            _context.vets.Remove(vet);
            _context.SaveChanges();
        }
    }


    public void AgregarVeterinario()
    {
        Console.WriteLine("\n --Agregar Veterinario--");

        Console.Write("Por favor ingresa el nombre: ");
        string NewName = Console.ReadLine();

        Console.Write("Ingresa el documento: ");
        string NewDocument = Console.ReadLine();

        Console.Write("Ingresa el email: ");
        string NewEmail = Console.ReadLine();

        Console.Write("Ingresa el numero de atenciones: ");
        int NewAttentions = Convert.ToInt32(Console.ReadLine());

        var vets = new Vet(NewName, NewDocument, NewEmail, NewAttentions);

        Add(vets);

        Console.WriteLine("Veterinario agregado con exito!");
    }


    public void ListarVeterinarios()
    {
        var veterinario = GetAll();

        foreach (var v in veterinario)
        {
            Console.WriteLine($"ID: {v.Id} | Nombre: {v.Name} | Documento: {v.Document} | Atenciones: {v.Atentions}");
        }
    }

    public void BuscarVetPorID()
    {
        Console.Write("Ingrese el ID el veterinario: ");
        int IdVet = Convert.ToInt32(Console.ReadLine());

        var BuscarVet = GetById(IdVet);
        if (BuscarVet != null)
        {
            Console.WriteLine($"Encontrado: {BuscarVet.Name}, {BuscarVet.Email}");
        }
        else
        {
            Console.WriteLine("Veterinario no encontrado!");
        }
    }


    public void EditarVeterinario()
    {
        Console.Write("Ingrese el ID a editar: ");
        int IdEditar = int.Parse(Console.ReadLine());

        var vetEditar = GetById(IdEditar);
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



    public void Menu()
    {

        while (true)
        {
            Console.WriteLine("\n--- Gestion de Veterinarios ---");
            Console.WriteLine("1. Agrega un nuevo veterinario.");
            Console.WriteLine("2. Listar los veterinarios");
            Console.WriteLine("3. Busca un veterinario por su id.");
            Console.WriteLine("4. Edita un veterinario.");
            Console.WriteLine("5. Elimina un veterinario");
            Console.WriteLine("0. Salir del menu.");
            Console.Write("Selecciona una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out int opcion))
            {
                Console.WriteLine("Opcion invalida, intente de nuevo");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    AgregarVeterinario();
                    break;
                case 2:
                    ListarVeterinarios();
                    break;
                case 3:
                    BuscarVetPorID();
                    break;
                case 4:
                    EditarVeterinario();
                    break;
                case 5:
                    EliminarVeterinario();
                    break;
                case 0:
                    Console.WriteLine("Saliendo del menu");
                    return;
                default:
                    Console.WriteLine("Opcion Invalida");
                    continue;
            }
        }
    }
}
