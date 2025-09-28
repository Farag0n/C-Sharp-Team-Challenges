namespace SisVetSanMiguel.Domain.Interfaces;


public interface IGeneralCrud<T>
{
    void GetAll();   // Listar todos
    void GetById(int id);        // Buscar por id
    void Add(T entity);        // Crear
    void Update(T entity, int id);     // Actualizar
    void Delete(int id);       // Eliminar
}