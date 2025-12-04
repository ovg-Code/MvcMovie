using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    public class AddressesController : Controller
    {
        private readonly IAddressService _service;
        private readonly IActorService _actorService;
        private readonly IAddressTypeService _addressTypeService;
        private readonly IZipCodeService _zipCodeService;

        public AddressesController(IAddressService service,
            IActorService actorService,
            IAddressTypeService addressTypeService,
            IZipCodeService zipCodeService)
        {
            _service = service;
            _actorService = actorService;
            _addressTypeService = addressTypeService;
            _zipCodeService = zipCodeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var address = await _service.GetByIdAsync(id.Value);
            if (address == null) return NotFound();
            return View(address);
        }

        public async Task<IActionResult> Create()
        {
            await LoadDropdownDataAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Address address)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(address);
                return RedirectToAction(nameof(Index));
            }
            
            await LoadDropdownDataAsync();
            return View(address);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var address = await _service.GetByIdAsync(id.Value);
            if (address == null) return NotFound();
            
            await LoadDropdownDataAsync();
            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Address address)
        {
            if (id != address.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(address);
                return RedirectToAction(nameof(Index));
            }
            
            await LoadDropdownDataAsync();
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
            try
            {
                await _service.DeleteAsync(id);
                TempData["SuccessMessage"] = "Address eliminado exitosamente.";
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
            var addressTypes = await _addressTypeService.GetAllAsync();
            var zipCodes = await _zipCodeService.GetAllAsync();
            ViewBag.Actors = new SelectList(actors, "Id", "FirstFirstName");
            ViewBag.AddressTypes = new SelectList(addressTypes, "Id", "Name");
            ViewBag.ZipCodes = new SelectList(zipCodes, "Id", "Name");
        }
    }
}
