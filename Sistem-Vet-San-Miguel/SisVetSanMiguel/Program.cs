
using SisVetSanMiguel;
using SisVetSanMiguel.Domain.Models;
using SisVetSanMiguel.Services;

namespace MyNamespace;

public class Program
{
    public static void Main(string[] args)
    {
        //instanciar el contexto de la base de datos
        var context = new AppDbContext();
        
        // Instanciar los servicios
        var petService = new PetService(context);
        var vetService = new VetService(context);
        var clientService = new ClientServices(context);
        var atentionService = new AtentionServices(context);
        var querieService = new QuerieService(context);
        
        var client1 = new Client("Miguel", "232344456", "miguel@email.com", 1);
        var client2 = new Client("David", "3456455463", "awooo@email.com", 2);
        
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("   🐾 Sistema Veterinaria San Miguel ");
            Console.WriteLine("=====================================");
            Console.WriteLine("1. Menú de Mascotas");
            Console.WriteLine("2. Menú de Veterinarios");
            Console.WriteLine("3. Menú de Clientes");
            Console.WriteLine("4. Menú de Atenciones");
            Console.WriteLine("5. Menú de Consultas");
            Console.WriteLine("0. Salir");
            Console.WriteLine("=====================================");
            Console.Write("Seleccione una opción: ");

            string option = Console.ReadLine() ?? "";

            switch (option)
            {
                case "1":
                    PetService.PetMenu();
                    break;
                case "2":
                    VetService.VetMenu(vetService);
                    break;
                case "3":
                    ClientService.ClientMenu(clientService);
                    break;
                case "4":
                    AtentionServices.AtentionMenu(atentionService);
                    break;
                case "5":
                    QuerieService.QuerieMenu(querieService);
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Saliendo del sistema...");
                    break;
                default:
                    Console.WriteLine("Opción inválida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}