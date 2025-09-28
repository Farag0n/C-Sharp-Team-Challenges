namespace SisVetSanMiguel.Services;

//Se "importa" el codigo que se va a usar
using SisVetSanMiguel.Domain.Interfaces;
using SisVetSanMiguel.Domain.Models;
using System.Collections.Generic;
using System.Linq;

public class PetService : IGeneralCrud<Pet>
{

    //Esto hace ......
    private readonly AppDbContext _context;
    
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
                case "1": RegisterPet();
                    break;
                case "2": ListPets();
                    break;
                case "3": EditPet();
                    break;
                case "4": DeletePet();
                    break;
                case "5": state = false;
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
        Console.WriteLine("- 1 Registrar mascota\n- 2 Ver mascotas registradas\n- 3 Editar mascota\n- 4 Eliminar mascota\n- 5 Volver al menu principal");
    }
    //--------------------------------------------------------------------------------------------------
    
    //Metodo de registro de mascota
    //--------------------------------------------------------------------------------------------------
    private static void RegisterPet()
    {
        
    } 
    //--------------------------------------------------------------------------------------------------
    
    //Metodo de mostrar lista de mascotas
    //--------------------------------------------------------------------------------------------------
    {
    }
    //--------------------------------------------------------------------------------------------------
    
    //Metodo para editar mascota
    //--------------------------------------------------------------------------------------------------
    private static void EditPet()
    {
        
    }
    //--------------------------------------------------------------------------------------------------
    
    //Metodo para eliminar mascota
    //--------------------------------------------------------------------------------------------------
    private static void DeletePet()
    {
        
    }
    //--------------------------------------------------------------------------------------------------
}