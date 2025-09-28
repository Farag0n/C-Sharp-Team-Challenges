namespace SisVetSanMiguel.Domain.Models;
public abstract class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }

    public Person(string name, string document, string email)
    {
        Name = name;
        Document = document;
        Email = email;
    }
    
    
}
