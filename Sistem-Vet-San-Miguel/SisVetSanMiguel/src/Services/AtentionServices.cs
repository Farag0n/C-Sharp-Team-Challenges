using SisVetSanMiguel.Domain.Models;
using SisVetSanMiguel.Domain.Interfaces;
namespace SisVetSanMiguel.Services;

public class AtentionServices : IGeneralCrud<Atention>
{
    public readonly AppDbContext _context;
    
    public AtentionServices(AppDbContext context)
    {
        _context = context;
    }

    // Listar todas las atenciones
    public void GetAll()
    {
        var atentions = _context.atentions.ToList();
        foreach (var atention in atentions)
        {
            Console.WriteLine($"id: {atention.Id}, Client id: {atention.ClientId}");
        }
    }   

    // Buscar por id
    public void GetById(int id)
    {
        var atention = _context.atentions.Find(id);
        if (atention != null)
        {
            Console.WriteLine($"id: {atention.Id}, Client id: {atention.ClientId}");
        }
        
    }        

    // Crear atencion
    public void Add(Atention entity)
    {
        _context.atentions.Add(entity);
        _context.SaveChanges();
    }        

    // Actualizar atencion
    public void Update(Atention entity, int id)
    {
        var atention = _context.atentions.Find(id);
        if (atention != null)
        {
            atention.ClientId = entity.ClientId;
            atention.Date = entity.Date;
            atention.MedicalReport = entity.MedicalReport;
            atention.PetId = entity.PetId;
            atention.VetId = entity.VetId;
            
            _context.SaveChanges();
        }
        
    }     

    // Eliminar atencion
    public void Delete(int id)
    {
        var atention = _context.atentions.Find(id);
        if (atention != null)
        {
            _context.atentions.Remove(atention);
            _context.SaveChanges();
        }
        
    }
}