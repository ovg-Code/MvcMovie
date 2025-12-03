using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

/// <summary>
/// Implementa la logica de negocio para la gestion de clientes.
/// </summary>
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _customerRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Customer>> GetActiveCustomersAsync()
    {
        return await _customerRepository.GetActiveCustomersAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(Guid id)
    {
        return await _customerRepository.GetByIdAsync(id);
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        // Aquí puedes agregar validaciones de negocio
        // Por ejemplo: validar que no exista email duplicado, etc.
        
        return await _customerRepository.AddAsync(customer);
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        // Aquí puedes agregar validaciones de negocio
        
        await _customerRepository.UpdateAsync(customer);
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
        await _customerRepository.DeleteAsync(id);
    }

    public async Task<bool> CustomerExistsAsync(Guid id)
    {
        return await _customerRepository.ExistsAsync(id);
    }
}
