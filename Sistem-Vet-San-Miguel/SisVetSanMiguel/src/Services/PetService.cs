using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SisVetSanMiguel.Services;

//Se "importa" el codigo que se va a usar
using SisVetSanMiguel.Domain.Interfaces;
using SisVetSanMiguel.Domain.Models;


public class PetService : IGeneralCrud<Pet>
{

    //Esto hace ......
    public readonly AppDbContext _context;
    
    //Este constructor es para .....
    public PetService(AppDbContext context)
    {
        _context = context;
    }
    
    //Metodo del menu
    //--------------------------------------------------------------------------------------------------
    public static void PetMenu()
    {
        //variable para poder salir de este sub menu
        bool state = true;
        
        while (state)
        {
            VisualMenu();

            //Variable para almacecanr la opcion
            string option = Console.ReadLine();

            switch (option)
            {
                case "1": Add();
                    break;
                case "2": GetAll();
                    break;
                case "3": GetById();
                    break;
                case "4": Update();
                    break;
                case "5": Delete();
                    break;
                case "6": state = false;
                    break;
                default: Console.WriteLine("La opcion no es valida, intente de nuevo");
                    
            }
        }
    }
    //--------------------------------------------------------------------------------------------------
    
    //Metodo de bienvenida de menu
    //--------------------------------------------------------------------------------------------------
    private static void VisualMenu()
    {
        //Mensage de bienvenida
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("==== Bienvenido a el menu de mascota ===");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("=== Digite la opcion que desea ===");
        Console.WriteLine("- 1 Registrar mascota\n- 2 Ver mascotas registradas\n- 3 Buscar mascota por ID\n- 4 Editar mascota\n- Eliminar mascota por ID\n- 6 Volver al menu principal");
    }
    //--------------------------------------------------------------------------------------------------
    
    //Metodo de registro de mascota
    //--------------------------------------------------------------------------------------------------
    public void Add(Pet entity)
    {
        _context.pets.Add(entity);
        _context.SaveChanges();
    } 
    //--------------------------------------------------------------------------------------------------
    
    //Metodo de mostrar lista de mascotas
    //--------------------------------------------------------------------------------------------------
    public void GetAll()
    {
        var pets = _context.pets.ToList();
        
        foreach (var pet in pets)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Nombre: {pet.Name}\nEspecie: {pet.Specie}\nRaza: {pet.Race}\nId Dueño: {pet.IdPetOwner}");
            Console.WriteLine("------------------------------------");
        }
    }
    //--------------------------------------------------------------------------------------------------
    
    //Metodo para buscar mascota por ID
    //--------------------------------------------------------------------------------------------------
    public void GetById(int id)
    {
        var pet = _context.pets.Find(id);
        
        if (pet != null)
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"Nombre: {pet.Name}\nEspecie: {pet.Specie}\nRaza: {pet.Race}\nDueño: {pet.IdPetOwner}");
            Console.WriteLine("-----------------------------------------");
        }
        else
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("La mascota no fue encontrada");
            Console.WriteLine("----------------------------");
        }
    }
    //--------------------------------------------------------------------------------------------------
    
    //Metodo para editar mascota
    //--------------------------------------------------------------------------------------------------
    public void Update(Pet entity, int id)
    {
        var existingPet = _context.pets.Find(id);
        
        if (existingPet != null)
        {
            existingPet.Name = entity.Name;
            existingPet.Specie = entity.Specie;
            existingPet.Race = entity.Race;
            existingPet.IdPetOwner = entity.IdPetOwner;

            _context.SaveChanges();
        }
    }
    //--------------------------------------------------------------------------------------------------
    
    //Metodo para eliminar mascota
    //--------------------------------------------------------------------------------------------------
    public void void Delete(int id)
    {
        var pet = _context.pets.Find(id);

        if (pet != null)
        {
            _context.pets.Remove(pet);
            _context.SaveChanges();
        }
    }
    //--------------------------------------------------------------------------------------------------
}