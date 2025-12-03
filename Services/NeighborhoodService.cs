using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

/// <summary>
/// Implementa la logica de negocio para el catalogo de barrios.
/// </summary>
public class NeighborhoodService : INeighborhoodService
{
    private readonly INeighborhoodRepository _repository;

    public NeighborhoodService(INeighborhoodRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Neighborhood>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Neighborhood?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Neighborhood> CreateAsync(Neighborhood entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(Neighborhood entity)
    {
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _repository.ExistsAsync(id);
    }
}
