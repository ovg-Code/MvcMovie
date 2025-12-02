using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    public class AddressTypesController : Controller
    {
        private readonly IAddressTypeService _service;

        public AddressTypesController(IAddressTypeService service)
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
            var addressType = await _service.GetByIdAsync(id.Value);
            if (addressType == null) return NotFound();
            return View(addressType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] AddressType addressType)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(addressType);
                return RedirectToAction(nameof(Index));
            }
            return View(addressType);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var addressType = await _service.GetByIdAsync(id.Value);
            if (addressType == null) return NotFound();
            return View(addressType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] AddressType addressType)
        {
            if (id != addressType.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(addressType);
                return RedirectToAction(nameof(Index));
            }
            return View(addressType);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var addressType = await _service.GetByIdAsync(id.Value);
            if (addressType == null) return NotFound();
            return View(addressType);
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
