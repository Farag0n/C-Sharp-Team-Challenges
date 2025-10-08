using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Game_Catalog.Models;
using Game_Catalog.Infraestructure;

namespace Game_Catalog.Controllers;

public class GameController : Controller
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public GameController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<IActionResult> Index()
    {
        var games = _context.Games.ToList();
        return View(games);
    }
    

    // Añadir juego
    [HttpPost]
    public async Task<IActionResult> AddGames(string nameGame)
    {
        Console.WriteLine("111111");
        string apiKey = _config["ApiKey"]!;
        string apiUrl = $"https://api.rawg.io/api/games?search={nameGame}&key={apiKey}";
        try
        {
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync(apiUrl);

            JsonDocument doc = JsonDocument.Parse(result);
            var name = doc.RootElement.GetProperty("name");
            var genres = doc.RootElement.GetProperty("genres");
            var image = doc.RootElement.GetProperty("background_image");

            return RedirectToAction("Index");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.InnerException.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return RedirectToAction("Index");
    }
    
    
    
    
}