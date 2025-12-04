using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    public class PhonesController : Controller
    {
        private readonly IPhoneService _service;
        private readonly IActorService _actorService;
        private readonly IPhoneTypeService _phoneTypeService;

        public PhonesController(IPhoneService service,
            IActorService actorService,
            IPhoneTypeService phoneTypeService)
        {
            _service = service;
            _actorService = actorService;
            _phoneTypeService = phoneTypeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var phone = await _service.GetByIdAsync(id.Value);
            if (phone == null) return NotFound();
            return View(phone);
        }

        public async Task<IActionResult> Create()
        {
            await LoadDropdownDataAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Phone phone)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(phone);
                return RedirectToAction(nameof(Index));
            }
            
            await LoadDropdownDataAsync();
            return View(phone);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var phone = await _service.GetByIdAsync(id.Value);
            if (phone == null) return NotFound();
            
            await LoadDropdownDataAsync();
            return View(phone);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Phone phone)
        {
            if (id != phone.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(phone);
                return RedirectToAction(nameof(Index));
            }
            
            await LoadDropdownDataAsync();
            return View(phone);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var phone = await _service.GetByIdAsync(id.Value);
            if (phone == null) return NotFound();
            return View(phone);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                TempData["SuccessMessage"] = "Phone eliminado exitosamente.";
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
            var phoneTypes = await _phoneTypeService.GetAllAsync();
            ViewBag.Actors = new SelectList(actors, "Id", "FirstFirstName");
            ViewBag.PhoneTypes = new SelectList(phoneTypes, "Id", "Name");
        }
    }
}
