using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    /// <summary>
    /// Controlador MVC para la gestion de documentos de identidad.
    /// </summary>
    public class IdentityCardsController : Controller
    {
        private readonly IIdentityCardService _service;

        public IdentityCardsController(IIdentityCardService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var identityCards = await _service.GetAllAsync();
            return View(identityCards);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            
            var identityCard = await _service.GetByIdAsync(id.Value);
            if (identityCard == null) return NotFound();
            
            return View(identityCard);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActorsId,IdcardTypesId,Idcard,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] IdentityCard identityCard)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(identityCard);
                return RedirectToAction(nameof(Index));
            }
            return View(identityCard);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            
            var identityCard = await _service.GetByIdAsync(id.Value);
            if (identityCard == null) return NotFound();
            
            return View(identityCard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ActorsId,IdcardTypesId,Idcard,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] IdentityCard identityCard)
        {
            if (id != identityCard.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(identityCard);
                }
                catch (Exception)
                {
                    if (!await _service.ExistsAsync(identityCard.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(identityCard);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            
            var identityCard = await _service.GetByIdAsync(id.Value);
            if (identityCard == null) return NotFound();
            
            return View(identityCard);
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
