using ari2._0.Models;

namespace ari2._0.Services;

/// <summary>
/// Define las operaciones de negocio para la gestion de relaciones entre actores.
/// </summary>
public interface IActorRelationshipService
{
    Task<IEnumerable<ActorRelationship>> GetAllAsync();
    Task<ActorRelationship?> GetByIdAsync(Guid id);
    Task<ActorRelationship> CreateAsync(ActorRelationship entity);
    Task UpdateAsync(ActorRelationship entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
