using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    public class SocialNetworksController : Controller
    {
        private readonly ISocialNetworkService _service;

        public SocialNetworksController(ISocialNetworkService service)
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
            var socialNetwork = await _service.GetByIdAsync(id.Value);
            if (socialNetwork == null) return NotFound();
            return View(socialNetwork);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] SocialNetwork socialNetwork)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(socialNetwork);
                return RedirectToAction(nameof(Index));
            }
            return View(socialNetwork);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var socialNetwork = await _service.GetByIdAsync(id.Value);
            if (socialNetwork == null) return NotFound();
            return View(socialNetwork);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] SocialNetwork socialNetwork)
        {
            if (id != socialNetwork.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(socialNetwork);
                return RedirectToAction(nameof(Index));
            }
            return View(socialNetwork);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var socialNetwork = await _service.GetByIdAsync(id.Value);
            if (socialNetwork == null) return NotFound();
            return View(socialNetwork);
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
