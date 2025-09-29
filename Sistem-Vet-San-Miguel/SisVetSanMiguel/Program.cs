
using SisVetSanMiguel;
using SisVetSanMiguel.Domain.Models;
using SisVetSanMiguel.Services;

namespace MyNamespace;

public class Program
{
    public static void Main(string[] args)
    {
        var context = new AppDbContext();
        var clientServices = new ClientServices(context);
        var client1 = new Client("Miguel", "232344456", "miguel@email.com", 1);
        var client2 = new Client("David", "3456455463", "awooo@email.com", 2);
        // clientServices.Add(client1);
        clientServices.Update(client2, 2);
        clientServices.Delete(3);

        //clientServices.GetAll();

        var petService = new PetService(context);
        //petService.PetMenu();

        var queries = new QuerieService(context);
        queries.GetClientWithMostPets();
    }
}