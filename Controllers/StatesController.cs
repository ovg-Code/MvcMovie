using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    public class StatesController : Controller
    {
        private readonly IStateService _service;

        public StatesController(IStateService service)
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
            var state = await _service.GetByIdAsync(id.Value);
            if (state == null) return NotFound();
            return View(state);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CountryId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] State state)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(state);
                return RedirectToAction(nameof(Index));
            }
            return View(state);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var state = await _service.GetByIdAsync(id.Value);
            if (state == null) return NotFound();
            return View(state);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CountryId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] State state)
        {
            if (id != state.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(state);
                return RedirectToAction(nameof(Index));
            }
            return View(state);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var state = await _service.GetByIdAsync(id.Value);
            if (state == null) return NotFound();
            return View(state);
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
