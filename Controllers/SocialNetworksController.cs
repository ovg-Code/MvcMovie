using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    public class SocialNetworksController : Controller
    {
        private readonly ISocialNetworkService _service;
        private readonly IActorService _actorService;

        public SocialNetworksController(ISocialNetworkService service,
            IActorService actorService)
        {
            _service = service;
            _actorService = actorService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var socialNetwork = await _service.GetByIdAsync(id.Value);
            if (socialNetwork == null) return NotFound();
            return View(socialNetwork);
        }

        public async Task<IActionResult> Create()
        {
            await LoadDropdownDataAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialNetwork socialNetwork)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(socialNetwork);
                return RedirectToAction(nameof(Index));
            }
            
            await LoadDropdownDataAsync();
            return View(socialNetwork);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var socialNetwork = await _service.GetByIdAsync(id.Value);
            if (socialNetwork == null) return NotFound();
            
            await LoadDropdownDataAsync();
            return View(socialNetwork);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SocialNetwork socialNetwork)
        {
            if (id != socialNetwork.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(socialNetwork);
                return RedirectToAction(nameof(Index));
            }
            
            await LoadDropdownDataAsync();
            return View(socialNetwork);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var socialNetwork = await _service.GetByIdAsync(id.Value);
            if (socialNetwork == null) return NotFound();
            return View(socialNetwork);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                TempData["SuccessMessage"] = "SocialNetwork eliminado exitosamente.";
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
            ViewBag.Actors = new SelectList(actors, "Id", "FirstFirstName");
        }
    }
}
