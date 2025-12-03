using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

/// <summary>
/// Implementa la logica de negocio para el catalogo de tipos de direccion.
/// </summary>
public class AddressTypeService : IAddressTypeService
{
    private readonly IAddressTypeRepository _repository;

    public AddressTypeService(IAddressTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AddressType>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<AddressType?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<AddressType> CreateAsync(AddressType entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(AddressType entity)
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
