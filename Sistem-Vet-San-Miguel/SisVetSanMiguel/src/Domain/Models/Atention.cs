namespace SisVetSanMiguel.Domain.Models;

public class Atention
{
    public int Id { get; set; }
    public int VetId { get; set; }
    public int ClientId { get; set; }
    public int PetId { get; set; }
    public string Date { get; set; }
    public string MedicalReport { get; set; }
    
    public Atention() {}

    public Atention(int vetId, int clientId, int petId, string date, string medicalReport)
    {
        VetId = vetId;
        ClientId = clientId;
        PetId = petId;
        Date = date;
        MedicalReport = medicalReport;
    }
}
