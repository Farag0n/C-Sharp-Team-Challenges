namespace SisVetSanMiguel.Domain.Models;

public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public string Race { get; set; }

    public Pet(int id, string name, string species, string race)
    {
        Id = id;
        Name = name;
        Species = species;
        Race = race;
    }
}
