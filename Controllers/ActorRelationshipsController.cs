using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    public class ActorRelationshipsController : Controller
    {
        private readonly IActorRelationshipService _service;

        public ActorRelationshipsController(IActorRelationshipService service)
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
            var actorRelationship = await _service.GetByIdAsync(id.Value);
            if (actorRelationship == null) return NotFound();
            return View(actorRelationship);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentId,ChildId,RelationshipTypesId,IsPercentage,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] ActorRelationship actorRelationship)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(actorRelationship);
                return RedirectToAction(nameof(Index));
            }
            return View(actorRelationship);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var actorRelationship = await _service.GetByIdAsync(id.Value);
            if (actorRelationship == null) return NotFound();
            return View(actorRelationship);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ParentId,ChildId,RelationshipTypesId,IsPercentage,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] ActorRelationship actorRelationship)
        {
            if (id != actorRelationship.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(actorRelationship);
                return RedirectToAction(nameof(Index));
            }
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
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
