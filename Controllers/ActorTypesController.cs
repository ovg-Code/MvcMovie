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
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] ActorType actorType)
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] ActorType actorType)
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
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
