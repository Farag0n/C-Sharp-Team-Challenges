namespace SisVetSanMiguel.Services;

//Se "importa" el codigo que se va a usar
using SisVetSanMiguel.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

{
    //Metodo del menu
    //--------------------------------------------------------------------------------------------------
    public static void PetMenu()
    {
        //variable para poder salir de este sub menu
        bool state = true;
        
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
    
}