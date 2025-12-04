using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    /// <summary>
    /// Controlador MVC para el catalogo de tipos de actor.
    /// </summary>
    public class ActorTypesController : Controller
    {
        private readonly IActorTypeService _service;

        public ActorTypesController(IActorTypeService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var actorType = await _service.GetByIdAsync(id.Value);
            if (actorType == null) return NotFound();
            return View(actorType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActorType actorType)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(actorType);
                return RedirectToAction(nameof(Index));
            }
            return View(actorType);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var actorType = await _service.GetByIdAsync(id.Value);
            if (actorType == null) return NotFound();
            return View(actorType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ActorType actorType)
        {
            if (id != actorType.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(actorType);
                return RedirectToAction(nameof(Index));
            }
            return View(actorType);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var actorType = await _service.GetByIdAsync(id.Value);
            if (actorType == null) return NotFound();
            return View(actorType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                TempData["SuccessMessage"] = "Tipo de Actor eliminado exitosamente.";
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
    }
}
