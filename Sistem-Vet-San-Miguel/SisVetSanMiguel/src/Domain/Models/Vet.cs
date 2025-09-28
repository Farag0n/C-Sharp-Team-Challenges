namespace SisVetSanMiguel.Domain.Models;

public class Vet : Person
{
    public int Atentions { get; set; }
    
    public Vet(string nombre, string documento, string email, int atentions) 
        : base(nombre, documento, email)
    {
        Atentions = atentions;
    }
    
}
