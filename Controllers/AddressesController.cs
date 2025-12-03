using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    /// <summary>
    /// Controlador MVC para la gestion de direcciones.
    /// </summary>
    public class AddressesController : Controller
    {
        private readonly IAddressService _service;

        public AddressesController(IAddressService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var addresses = await _service.GetAllAsync();
            return View(addresses);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            
            var address = await _service.GetByIdAsync(id.Value);
            if (address == null) return NotFound();
            
            return View(address);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActorsId,AddressTypesId,ZipCodesId,Street,Apartment,IsVerified,Latitude,Longitude,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Address address)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(address);
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            
            var address = await _service.GetByIdAsync(id.Value);
            if (address == null) return NotFound();
            
            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ActorsId,AddressTypesId,ZipCodesId,Street,Apartment,IsVerified,Latitude,Longitude,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Address address)
        {
            if (id != address.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(address);
                }
                catch (Exception)
                {
                    if (!await _service.ExistsAsync(address.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            
            var address = await _service.GetByIdAsync(id.Value);
            if (address == null) return NotFound();
            
            return View(address);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
