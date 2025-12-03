using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    /// <summary>
    /// Controlador MVC para el catalogo de codigos postales.
    /// </summary>
    public class ZipCodesController : Controller
    {
        private readonly IZipCodeService _service;

        public ZipCodesController(IZipCodeService service)
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
            var zipCode = await _service.GetByIdAsync(id.Value);
            if (zipCode == null) return NotFound();
            return View(zipCode);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,NeighborhoodId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] ZipCode zipCode)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(zipCode);
                return RedirectToAction(nameof(Index));
            }
            return View(zipCode);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var zipCode = await _service.GetByIdAsync(id.Value);
            if (zipCode == null) return NotFound();
            return View(zipCode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Code,NeighborhoodId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] ZipCode zipCode)
        {
            if (id != zipCode.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(zipCode);
                return RedirectToAction(nameof(Index));
            }
            return View(zipCode);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var zipCode = await _service.GetByIdAsync(id.Value);
            if (zipCode == null) return NotFound();
            return View(zipCode);
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
