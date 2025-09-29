using System;
using SisVetSanMiguel;
using SisVetSanMiguel.Services;

public class Program
{
    static void Main(string[] args)
    {
        using var context = new AppDbContext();

        // Crear el servicio de veterinarios
        var vetService = new VeterinariaService(context);

        // Mostrar el menú de veterinarios
        vetService.Menu();
    }
}
