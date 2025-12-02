using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    public class MunicipalitiesController : Controller
    {
        private readonly IMunicipalityService _service;

        public MunicipalitiesController(IMunicipalityService service)
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
            var municipality = await _service.GetByIdAsync(id.Value);
            if (municipality == null) return NotFound();
            return View(municipality);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StateId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Municipality municipality)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(municipality);
                return RedirectToAction(nameof(Index));
            }
            return View(municipality);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var municipality = await _service.GetByIdAsync(id.Value);
            if (municipality == null) return NotFound();
            return View(municipality);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,StateId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Municipality municipality)
        {
            if (id != municipality.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(municipality);
                return RedirectToAction(nameof(Index));
            }
            return View(municipality);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var municipality = await _service.GetByIdAsync(id.Value);
            if (municipality == null) return NotFound();
            return View(municipality);
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
