using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.Models;
using VideoGameCatalog.Infraestructure;

namespace VideoGameCatalog.Controllers;

public class GameController : Controller
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public GameController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    //Mostrar todos los juegos
    //---------------------------------------------------------------------------------------
    public async Task<IActionResult> Index()
    {
        var games = _context.Games.ToList();
        return View(games);
    }
    

    //Agregar juegos version 2
    //---------------------------------------------------------------------------------------
    [HttpPost]
    public async Task<IActionResult> AddGames(string nameGame)
    {
        string apiKey = _config["ApiKey"]!;
        string apiUrl = $"https://api.rawg.io/api/games?search={nameGame}&key={apiKey}";

        try
        {
            //porque estos using y no un var
            using HttpClient client = new HttpClient();
            var result = await client.GetStringAsync(apiUrl);
            using JsonDocument doc = JsonDocument.Parse(result);

            // Acceder al primer resultado del array "results"
            var results = doc.RootElement.GetProperty("results");
            if (results.GetArrayLength() == 0)
            {
                //como retornar este error
                Console.WriteLine("No se encontraron resultados para el juego buscado.");
                return RedirectToAction("Index");
            }

            //esto para que es
            var firstGame = results[0];
            
            var name = firstGame.GetProperty("name").GetString();
            var image = firstGame.GetProperty("background_image").GetString();

            // Si el juego tiene géneros, los concatenamos
            string genres = "";
            if (firstGame.TryGetProperty("genres", out var genresArray))
            {
                genres = string.Join(", ", genresArray.EnumerateArray().Select(g => g.GetProperty("name").GetString()));
            }

            //guardar en el objeto
            var game = new Game(name, genres, 0, image);
            _context.Games.Add(game);
            await _context.SaveChangesAsync(); // ¡no olvide guardar!

            return RedirectToAction("Index");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error al conectar con la API: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error inesperado: {e.Message}");
        }

        return RedirectToAction("Index");
    }

    
    // Ver más
    //---------------------------------------------------------------------------------------
    [HttpGet]
    public async Task<IActionResult> ViewMore(int id)
    {
        var game = await _context.Games.FirstOrDefaultAsync(g => g.ID == id);
        if (game == null)
            return NotFound();

        return View(game);
    }

    
    //Editar juego
    //---------------------------------------------------------------------------------------
    //mostrar la vista de editar
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var game = await _context.Games.FirstOrDefaultAsync(g => g.ID == id);
        if (game == null)
            return NotFound();

        return View(game);
    }
    //Sobre escritura para hacer el post pero no estoy seguro si esto es polimorfismo
    [HttpPost]
    public async Task<IActionResult> Edit(Game game)
    {
        if (!ModelState.IsValid)
            return View(game);

        var existingGame = await _context.Games.FirstOrDefaultAsync(g => g.ID == game.ID);
        if (existingGame == null)
            return NotFound();

        existingGame.Name = game.Name;
        existingGame.Gender = game.Gender;
        existingGame.Price = game.Price;

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    
    //Eliminar juego
    //---------------------------------------------------------------------------------------
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var game = _context.Games.FirstOrDefaultAsync(g => g.ID == id);
        if (game == null)
            return NotFound();

        _context.Games.Remove(await game);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}