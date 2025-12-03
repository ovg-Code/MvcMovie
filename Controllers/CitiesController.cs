using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    /// <summary>
    /// Controlador MVC para el catalogo de ciudades.
    /// </summary>
    public class CitiesController : Controller
    {
        private readonly ICityService _service;

        public CitiesController(ICityService service)
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
            var city = await _service.GetByIdAsync(id.Value);
            if (city == null) return NotFound();
            return View(city);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StateId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] City city)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(city);
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var city = await _service.GetByIdAsync(id.Value);
            if (city == null) return NotFound();
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,StateId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] City city)
        {
            if (id != city.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(city);
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var city = await _service.GetByIdAsync(id.Value);
            if (city == null) return NotFound();
            return View(city);
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
