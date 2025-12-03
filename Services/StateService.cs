using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

/// <summary>
/// Implementa la logica de negocio para el catalogo de estados.
/// </summary>
public class StateService : IStateService
{
    private readonly IStateRepository _repository;

    public StateService(IStateRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<State>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<State?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<State> CreateAsync(State entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(State entity)
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
