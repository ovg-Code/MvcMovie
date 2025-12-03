using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

/// <summary>
/// Implementa la logica de negocio para el catalogo de ciudades.
/// </summary>
public class CityService : ICityService
{
    private readonly ICityRepository _repository;

    public CityService(ICityRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<City>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<City?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<City> CreateAsync(City entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(City entity)
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
