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
    
    //Testear
    //---------------------------------------------------------------------------------------
    [HttpPost]
    public async Task<IActionResult> Test()
    {
        try
        {
            string image = "https://uploads1.wikiart.org/images/the-legend-of-zelda-tears-of-the-kingdom-cover.jpg";
            
            var game = new Game("Zelda", null, 30000, image);
            _context.Games.Add(game);

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
    
    

    // Añadir juego
    //---------------------------------------------------------------------------------------
    [HttpPost]
    public async Task<IActionResult> AddGames(string nameGame)
    {
        string apiKey = _config["ApiKey"]!;
        string apiUrl = $"https://api.rawg.io/api/games?search={nameGame}&key={apiKey}";
        try
        {
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync(apiUrl);

            JsonDocument doc = JsonDocument.Parse(result);
            var name = doc.RootElement.GetProperty("name").GetString();
            var genres = doc.RootElement.GetProperty("genres").GetString();
            var image = doc.RootElement.GetProperty("background_image").GetString();

            var game = new Game(name, null, 0, image);
            _context.Games.Add(game);

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
    
    //Ver mas
    //---------------------------------------------------------------------------------------
    [HttpPost]
    public async Task<IActionResult> ViewMore(int id)
    {
        var game = _context.Games.FirstOrDefaultAsync(g => g.ID == id);

        if (game == null)
            return NotFound();

        return View(game);
    }
    
    //Editar juego
    //---------------------------------------------------------------------------------------
    [HttpPost]
    public async Task<IActionResult> Edit(Game game)
    {
        var existingGame = await _context.Games.FirstOrDefaultAsync(g => g.ID == game.ID);
        if (existingGame == null)
            return NotFound();

        
        existingGame.Price = game.Price;
        existingGame.Gender = game.Gender;

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