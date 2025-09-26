namespace SisVetSanMiguel.Domain.Interfaces;

public interface IGeneralCrud<T>
{
    IEnumerable<T> GetAll();   // Listar todos
    T? GetById(int id);        // Buscar por id
    void Add(T entity);        // Crear
    void Update(T entity);     // Actualizar
    void Delete(int id);       // Eliminar
}