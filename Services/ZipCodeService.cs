using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

/// <summary>
/// Implementa la logica de negocio para el catalogo de codigos postales.
/// </summary>
public class ZipCodeService : IZipCodeService
{
    private readonly IZipCodeRepository _repository;

    public ZipCodeService(IZipCodeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ZipCode>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ZipCode?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<ZipCode> CreateAsync(ZipCode entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(ZipCode entity)
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
