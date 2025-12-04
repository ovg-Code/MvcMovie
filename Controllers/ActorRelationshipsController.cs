using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    public class ActorRelationshipsController : Controller
    {
        private readonly IActorRelationshipService _service;
        private readonly IActorService _actorService;
        private readonly IRelationshipTypeService _relationshipTypeService;

        public ActorRelationshipsController(
            IActorRelationshipService service,
            IActorService actorService,
            IRelationshipTypeService relationshipTypeService)
        {
            _service = service;
            _actorService = actorService;
            _relationshipTypeService = relationshipTypeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var actorRelationship = await _service.GetByIdAsync(id.Value);
            if (actorRelationship == null) return NotFound();
            return View(actorRelationship);
        }

        public async Task<IActionResult> Create()
        {
            await LoadDropdownDataAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActorRelationship actorRelationship)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(actorRelationship);
                return RedirectToAction(nameof(Index));
            }
            
            await LoadDropdownDataAsync();
            return View(actorRelationship);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var actorRelationship = await _service.GetByIdAsync(id.Value);
            if (actorRelationship == null) return NotFound();
            
            await LoadDropdownDataAsync();
            return View(actorRelationship);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ActorRelationship actorRelationship)
        {
            if (id != actorRelationship.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(actorRelationship);
                return RedirectToAction(nameof(Index));
            }
            
            await LoadDropdownDataAsync();
            return View(actorRelationship);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var actorRelationship = await _service.GetByIdAsync(id.Value);
            if (actorRelationship == null) return NotFound();
            return View(actorRelationship);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                TempData["SuccessMessage"] = "Relación eliminado exitosamente.";
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
            var relationshipTypes = await _relationshipTypeService.GetAllAsync();

            ViewBag.ParentActors = new SelectList(actors, "Id", "FirstFirstName");
            ViewBag.ChildActors = new SelectList(actors, "Id", "FirstFirstName");
            ViewBag.RelationshipTypes = new SelectList(relationshipTypes, "Id", "Name");
        }
    }
}
