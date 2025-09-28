namespace SisVetSanMiguel.Domain.Models;

public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Specie { get; set; }
    public string Race { get; set; }
    public int IdPetOwner { get; set; }

    public Pet() {}
    public Pet(int id, string name, string specie, string race,  int idPetOwner)
    {
        Id = id;
        Name = name;
        Specie = specie;
        Race = race;
        IdPetOwner = idPetOwner;
    }
}
