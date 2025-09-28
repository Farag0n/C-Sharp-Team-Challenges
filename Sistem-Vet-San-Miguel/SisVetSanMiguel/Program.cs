
using SisVetSanMiguel;
using SisVetSanMiguel.Domain.Models;
using SisVetSanMiguel.Services;

namespace MyNamespace;

public class Program
{
    public static void Main(string[] args)
    {
        
        // Prueba Crud Clients
        var context = new AppDbContext();
        var clientServices = new ClientServices(context);
        var client1 = new Client("Miguel", "232344456", "miguel@email.com", 1);
        var client2 = new Client("David", "3456455463", "awooo@email.com", 2);
        // clientServices.Add(client1);
        //clientServices.Update(client2, 2);
        //clientServices.Delete(3);
        //clientServices.GetAll();
        
        
        // Prueba Crud Atentions

        var atentionServices = new AtentionServices(context);
        var atention1 = new Atention(1, 1, 1, "2025 - 09 - 28", "El animal se encuentra bien");
        var atention2 = new Atention(2, 1, 1, "2025 - 09 - 28", "El animal se encuentra Muy mal");
        var atention3 = new Atention(1, 1, 1, "2025 - 09 - 28", "El animal se encuentra Muerto");
        atentionServices.Add(atention1);
        atentionServices.Add(atention2);
        atentionServices.Update(atention3, 1);
        atentionServices.GetAll();
    }
}