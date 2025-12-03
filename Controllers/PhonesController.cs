using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    /// <summary>
    /// Controlador MVC para la gestion de telefonos.
    /// </summary>
    public class PhonesController : Controller
    {
        private readonly IPhoneService _service;

        public PhonesController(IPhoneService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var phones = await _service.GetAllAsync();
            return View(phones);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            
            var phone = await _service.GetByIdAsync(id.Value);
            if (phone == null) return NotFound();
            
            return View(phone);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActorsId,PhoneTypesId,Extension,Number,IsVerified,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(phone);
                return RedirectToAction(nameof(Index));
            }
            return View(phone);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            
            var phone = await _service.GetByIdAsync(id.Value);
            if (phone == null) return NotFound();
            
            return View(phone);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ActorsId,PhoneTypesId,Extension,Number,IsVerified,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Phone phone)
        {
            if (id != phone.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(phone);
                }
                catch (Exception)
                {
                    if (!await _service.ExistsAsync(phone.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(phone);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            
            var phone = await _service.GetByIdAsync(id.Value);
            if (phone == null) return NotFound();
            
            return View(phone);
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
