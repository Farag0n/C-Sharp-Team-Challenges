using System;
using SisVetSanMiguel.Services;


namespace SisVetSanMiguel;
public class Program
{
    public static void Main()
    {
        var context = new AppDbContext();
        var clientServices = new ClientServices(context);
        
        clientServices.createClient("Miguel", "12345676", "miguel@gmail.com", 1);
        
    }
}