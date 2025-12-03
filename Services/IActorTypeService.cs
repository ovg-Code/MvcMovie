using ari2._0.Models;

namespace ari2._0.Services;

/// <summary>
/// Define las operaciones de negocio para el catalogo de tipos de actor.
/// </summary>
public interface IActorTypeService
{
    Task<IEnumerable<ActorType>> GetAllAsync();
    Task<ActorType?> GetByIdAsync(Guid id);
    Task<ActorType> CreateAsync(ActorType entity);
    Task UpdateAsync(ActorType entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
