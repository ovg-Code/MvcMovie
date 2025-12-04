using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    public class IdentityCardsController : Controller
    {
        private readonly IIdentityCardService _service;
        private readonly IActorService _actorService;
        private readonly IIdentityCardTypeService _identityCardTypeService;
        private readonly ICountryService _countryService;

        public IdentityCardsController(IIdentityCardService service,
            IActorService actorService,
            IIdentityCardTypeService identityCardTypeService,
            ICountryService countryService)
        {
            _service = service;
            _actorService = actorService;
            _identityCardTypeService = identityCardTypeService;
            _countryService = countryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var identityCard = await _service.GetByIdAsync(id.Value);
            if (identityCard == null) return NotFound();
            return View(identityCard);
        }

        public async Task<IActionResult> Create()
        {
            await LoadDropdownDataAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityCard identityCard)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(identityCard);
                return RedirectToAction(nameof(Index));
            }
            
            await LoadDropdownDataAsync();
            return View(identityCard);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var identityCard = await _service.GetByIdAsync(id.Value);
            if (identityCard == null) return NotFound();
            
            await LoadDropdownDataAsync();
            return View(identityCard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, IdentityCard identityCard)
        {
            if (id != identityCard.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(identityCard);
                return RedirectToAction(nameof(Index));
            }
            
            await LoadDropdownDataAsync();
            return View(identityCard);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var identityCard = await _service.GetByIdAsync(id.Value);
            if (identityCard == null) return NotFound();
            return View(identityCard);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                TempData["SuccessMessage"] = "IdentityCard eliminado exitosamente.";
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
            var identityCardTypes = await _identityCardTypeService.GetAllAsync();
            var countries = await _countryService.GetAllAsync();
            ViewBag.Actors = new SelectList(actors, "Id", "FirstFirstName");
            ViewBag.IdentityCardTypes = new SelectList(identityCardTypes, "Id", "Name");
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
        }
    }
}
