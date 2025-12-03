using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    /// <summary>
    /// Controlador MVC para el catalogo de barrios.
    /// </summary>
    public class NeighborhoodsController : Controller
    {
        private readonly INeighborhoodService _service;

        public NeighborhoodsController(INeighborhoodService service)
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
            var neighborhood = await _service.GetByIdAsync(id.Value);
            if (neighborhood == null) return NotFound();
            return View(neighborhood);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Code,MunicipalityId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Neighborhood neighborhood)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(neighborhood);
                return RedirectToAction(nameof(Index));
            }
            return View(neighborhood);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var neighborhood = await _service.GetByIdAsync(id.Value);
            if (neighborhood == null) return NotFound();
            return View(neighborhood);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Code,MunicipalityId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Neighborhood neighborhood)
        {
            if (id != neighborhood.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(neighborhood);
                return RedirectToAction(nameof(Index));
            }
            return View(neighborhood);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var neighborhood = await _service.GetByIdAsync(id.Value);
            if (neighborhood == null) return NotFound();
            return View(neighborhood);
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
