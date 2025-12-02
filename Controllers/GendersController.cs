using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    public class GendersController : Controller
    {
        private readonly IGenderService _service;

        public GendersController(IGenderService service)
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
            var gender = await _service.GetByIdAsync(id.Value);
            if (gender == null) return NotFound();
            return View(gender);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Gender gender)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(gender);
                return RedirectToAction(nameof(Index));
            }
            return View(gender);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var gender = await _service.GetByIdAsync(id.Value);
            if (gender == null) return NotFound();
            return View(gender);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Gender gender)
        {
            if (id != gender.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(gender);
                return RedirectToAction(nameof(Index));
            }
            return View(gender);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var gender = await _service.GetByIdAsync(id.Value);
            if (gender == null) return NotFound();
            return View(gender);
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
