using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ari2._0.Models;
using ari2._0.Services;
using ari2._0.ViewModels;
using ari2._0.Data;

namespace ari2._0.Controllers
{
    public class ActorRelationshipsController : Controller
    {
        private readonly IActorRelationshipService _service;
        private readonly ApplicationDbContext _context;

        public ActorRelationshipsController(IActorRelationshipService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
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
            var actors = await _context.Actors.Where(a => a.IsEnabled == true).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstFirstName + " " + a.LastFirstName }).ToListAsync();
            var viewModel = new ActorRelationshipCreateViewModel
            {
                ParentActors = actors,
                ChildActors = actors,
                RelationshipTypes = await _context.RelationshipTypes.Where(r => r.IsEnabled == true).Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name }).ToListAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActorRelationshipCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var actorRelationship = new ActorRelationship
                {
                    ParentId = viewModel.ParentId,
                    ChildId = viewModel.ChildId,
                    RelationshipTypesId = viewModel.RelationshipTypesId,
                    IsPercentage = viewModel.IsPercentage,
                    IsEnabled = viewModel.IsEnabled
                };
                await _service.CreateAsync(actorRelationship);
                return RedirectToAction(nameof(Index));
            }
            
            var actors = await _context.Actors.Where(a => a.IsEnabled == true).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstFirstName + " " + a.LastFirstName }).ToListAsync();
            viewModel.ParentActors = actors;
            viewModel.ChildActors = actors;
            viewModel.RelationshipTypes = await _context.RelationshipTypes.Where(r => r.IsEnabled == true).Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name }).ToListAsync();
            return View(viewModel);
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
        public async Task<IActionResult> Edit(Guid id, ActorRelationship actorRelationship)
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
