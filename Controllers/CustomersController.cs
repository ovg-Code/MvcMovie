using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _service;
        private readonly IActorService _actorService;
        private readonly ICustomerPublicStatusTypeService _customerPublicStatusTypeService;

        public CustomersController(
            ICustomerService service,
            IActorService actorService,
            ICustomerPublicStatusTypeService customerPublicStatusTypeService)
        {
            _service = service;
            _actorService = actorService;
            _customerPublicStatusTypeService = customerPublicStatusTypeService;
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

        public async Task<IActionResult> Create()
        {
            await LoadDropdownDataAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            
            await LoadDropdownDataAsync();
            return View(customer);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var customer = await _service.GetCustomerByIdAsync(id.Value);
            if (customer == null) return NotFound();
            
            await LoadDropdownDataAsync();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Customer customer)
        {
            if (id != customer.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            
            await LoadDropdownDataAsync();
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
            try
            {
                await _service.DeleteCustomerAsync(id);
                TempData["SuccessMessage"] = "Cliente eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex) 
                when (ex.InnerException is Npgsql.PostgresException pgEx && pgEx.SqlState == "23503")
            {
                TempData["ErrorMessage"] = "No se puede eliminar este registro porque tiene datos relacionados. Primero debe eliminar o reasignar los registros relacionados.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al eliminar: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        private async Task LoadDropdownDataAsync()
        {
            var actors = await _actorService.GetAllAsync();
            var statusTypes = await _customerPublicStatusTypeService.GetAllAsync();
            
            ViewBag.Actors = new SelectList(actors, "Id", "FirstFirstName");
            ViewBag.CustomerPublicStatusTypes = new SelectList(statusTypes, "Id", "Name");
        }
    }
}
