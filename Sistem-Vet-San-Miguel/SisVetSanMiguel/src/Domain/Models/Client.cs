namespace SisVetSanMiguel.Domain.Models;

public class Client : Person
{
    public int IdPet { get; set; }
    
    public Client() {}
    public Client(string name, string document, string email, int idPet) 
        : base(name, document, email)
    {
        IdPet = idPet;
    }
    
}
