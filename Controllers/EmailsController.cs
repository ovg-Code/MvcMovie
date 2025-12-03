using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    /// <summary>
    /// Controlador MVC para la gestion de emails.
    /// </summary>
    public class EmailsController : Controller
    {
        private readonly IEmailService _service;

        public EmailsController(IEmailService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var emails = await _service.GetAllAsync();
            return View(emails);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            
            var email = await _service.GetByIdAsync(id.Value);
            if (email == null) return NotFound();
            
            return View(email);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActorsId,Definition,IsNotification,IsUnsuscribed,UnsuscribedAt,IsFailed,FaliledAt,IsPrimary,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Email email)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(email);
                return RedirectToAction(nameof(Index));
            }
            return View(email);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            
            var email = await _service.GetByIdAsync(id.Value);
            if (email == null) return NotFound();
            
            return View(email);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ActorsId,Definition,IsNotification,IsUnsuscribed,UnsuscribedAt,IsFailed,FaliledAt,IsPrimary,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Email email)
        {
            if (id != email.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(email);
                }
                catch (Exception)
                {
                    if (!await _service.ExistsAsync(email.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(email);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            
            var email = await _service.GetByIdAsync(id.Value);
            if (email == null) return NotFound();
            
            return View(email);
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
