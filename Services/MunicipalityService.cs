using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

/// <summary>
/// Implementa la logica de negocio para el catalogo de municipios.
/// </summary>
public class MunicipalityService : IMunicipalityService
{
    private readonly IMunicipalityRepository _repository;

    public MunicipalityService(IMunicipalityRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Municipality>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Municipality?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Municipality> CreateAsync(Municipality entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(Municipality entity)
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
