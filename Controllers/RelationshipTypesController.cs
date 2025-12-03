using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    /// <summary>
    /// Controlador MVC para el catalogo de tipos de relacion.
    /// </summary>
    public class RelationshipTypesController : Controller
    {
        private readonly IRelationshipTypeService _service;

        public RelationshipTypesController(IRelationshipTypeService service)
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
            var type = await _service.GetByIdAsync(id.Value);
            if (type == null) return NotFound();
            return View(type);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ParentId,SystemName,IsPercentage,IsAllowed,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] RelationshipType type)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(type);
                return RedirectToAction(nameof(Index));
            }
            return View(type);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var type = await _service.GetByIdAsync(id.Value);
            if (type == null) return NotFound();
            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ParentId,SystemName,IsPercentage,IsAllowed,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] RelationshipType type)
        {
            if (id != type.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(type);
                return RedirectToAction(nameof(Index));
            }
            return View(type);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var type = await _service.GetByIdAsync(id.Value);
            if (type == null) return NotFound();
            return View(type);
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
