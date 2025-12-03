using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

/// <summary>
/// Implementa la logica de negocio para la gestion de telefonos.
/// </summary>
public class PhoneService : IPhoneService
{
    private readonly IPhoneRepository _repository;

    public PhoneService(IPhoneRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Phone>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Phone?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Phone> CreateAsync(Phone entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(Phone entity)
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
