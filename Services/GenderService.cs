using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

/// <summary>
/// Implementa la logica de negocio para el catalogo de generos.
/// </summary>
public class GenderService : IGenderService
{
    private readonly IGenderRepository _repository;

    public GenderService(IGenderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Gender>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Gender?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Gender> CreateAsync(Gender entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(Gender entity)
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
