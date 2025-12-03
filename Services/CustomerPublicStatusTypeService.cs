using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

/// <summary>
/// Implementa la logica de negocio para el catalogo de estados de cliente.
/// </summary>
public class CustomerPublicStatusTypeService : ICustomerPublicStatusTypeService
{
    private readonly ICustomerPublicStatusTypeRepository _repository;

    public CustomerPublicStatusTypeService(ICustomerPublicStatusTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CustomerPublicStatusType>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<CustomerPublicStatusType?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<CustomerPublicStatusType> CreateAsync(CustomerPublicStatusType entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(CustomerPublicStatusType entity)
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
