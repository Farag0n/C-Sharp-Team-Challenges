namespace Game_Catalog.Models;

public class Game
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string? Gender { get; set; }
    public double? Price { get; set; }
    
    public string Image { get; set; }
    
    
    //Construtor vacio para evitar problemas de instanciacion con EF
    public Game (){}

    public Game(string name, string? gender, double? price, string image)
    {
        Name = name;
        Gender = gender;
        Price = price;
        Image = image;
    }
    
}