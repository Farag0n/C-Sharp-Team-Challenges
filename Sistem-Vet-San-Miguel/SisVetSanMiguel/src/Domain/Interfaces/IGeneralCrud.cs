namespace SisVetSanMiguel.Domain.Interfaces;


public interface IGeneralCrud<T>
{
    
    //Estructura para una interfaz general para todos los servicios
    IEnumerable<T> GetAll();   // Listar todos
    T? GetById(int id);        // Buscar por id
    void Add(T entity);        // Crear
    void Update(T entity);     // Actualizar
    void Delete(int id);       // Eliminar
}