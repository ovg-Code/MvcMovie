using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ari2._0.Models;
using ari2._0.Services;
using ari2._0.ViewModels;
using ari2._0.Data;

namespace ari2._0.Controllers
{
    /// <summary>
    /// Controlador MVC para la gestion de clientes.
    /// Implementa mejor práctica de Microsoft: usar ViewModels con SelectListItem
    /// en lugar de ViewBag/ViewData para poblar dropdowns.
    /// Referencia: https://learn.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms
    /// </summary>
    public class CustomersController : Controller
    {
        private readonly ICustomerService _service;
        private readonly ApplicationDbContext _context;

        public CustomersController(ICustomerService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllCustomersAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var customer = await _service.GetCustomerByIdAsync(id.Value);
            if (customer == null) return NotFound();
            return View(customer);
        }

        /// <summary>
        /// GET: Customers/Create
        /// Pobla los dropdowns usando ViewModel (mejor práctica vs ViewBag/ViewData).
        /// Filtra solo registros activos (IsEnabled = true) para mejor UX.
        /// </summary>
        public async Task<IActionResult> Create()
        {
            var viewModel = new CustomerCreateViewModel
            {
                Actors = await _context.Actors
                    .Where(a => a.IsEnabled == true)
                    .Select(a => new SelectListItem 
                    { 
                        Value = a.Id.ToString(), 
                        Text = a.FirstFirstName + " " + a.LastFirstName
                    })
                    .ToListAsync(),
                    
                CustomerPublicStatusTypes = await _context.CustomerPublicStatusTypes
                    .Where(c => c.IsEnabled == true)
                    .Select(c => new SelectListItem 
                    { 
                        Value = c.Id.ToString(), 
                        Text = c.Name 
                    })
                    .ToListAsync()
            };
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    ActorsId = viewModel.ActorsId,
                    CustomerPublicStatusTypesId = viewModel.CustomerPublicStatusTypesId,
                    IsAgentRetention = viewModel.IsAgentRetention,
                    IsLeasing = viewModel.IsLeasing,
                    IsEnabled = viewModel.IsEnabled,
                    OtherData = viewModel.OtherData
                };
                
                await _service.CreateCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            
            // Si hay errores de validación, repoblar los dropdowns
            viewModel.Actors = await _context.Actors
                .Where(a => a.IsEnabled == true)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstFirstName + " " + a.LastFirstName })
                .ToListAsync();
                
            viewModel.CustomerPublicStatusTypes = await _context.CustomerPublicStatusTypes
                .Where(c => c.IsEnabled == true)
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToListAsync();
            
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var customer = await _service.GetCustomerByIdAsync(id.Value);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ActorsId,CustomerPublicStatusTypesId,IsAgentRetention,IsLeasing,OtherData,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Customer customer)
        {
            if (id != customer.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var customer = await _service.GetCustomerByIdAsync(id.Value);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteCustomerAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
