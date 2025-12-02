using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    public class PhoneTypesController : Controller
    {
        private readonly IPhoneTypeService _service;

        public PhoneTypesController(IPhoneTypeService service)
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
            var phoneType = await _service.GetByIdAsync(id.Value);
            if (phoneType == null) return NotFound();
            return View(phoneType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] PhoneType phoneType)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(phoneType);
                return RedirectToAction(nameof(Index));
            }
            return View(phoneType);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var phoneType = await _service.GetByIdAsync(id.Value);
            if (phoneType == null) return NotFound();
            return View(phoneType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] PhoneType phoneType)
        {
            if (id != phoneType.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(phoneType);
                return RedirectToAction(nameof(Index));
            }
            return View(phoneType);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var phoneType = await _service.GetByIdAsync(id.Value);
            if (phoneType == null) return NotFound();
            return View(phoneType);
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
