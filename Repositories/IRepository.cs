namespace ari2._0.Repositories;

/// <summary>
/// Interfaz generica para operaciones CRUD de acceso a datos.
/// </summary>
/// <typeparam name="T">Tipo de entidad.</typeparam>
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
