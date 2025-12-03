using ari2._0.Models;

namespace ari2._0.Services;

/// <summary>
/// Define las operaciones de negocio para el catalogo de tipos de relacion.
/// </summary>
public interface IRelationshipTypeService
{
    Task<IEnumerable<RelationshipType>> GetAllAsync();
    Task<RelationshipType?> GetByIdAsync(Guid id);
    Task<RelationshipType> CreateAsync(RelationshipType entity);
    Task UpdateAsync(RelationshipType entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
