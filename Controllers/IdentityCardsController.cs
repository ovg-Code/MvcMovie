using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ari2._0.Models;
using ari2._0.Services;
using ari2._0.ViewModels;
using ari2._0.Data;

namespace ari2._0.Controllers
{
    public class IdentityCardsController : Controller
    {
        private readonly IIdentityCardService _service;
        private readonly ApplicationDbContext _context;

        public IdentityCardsController(IIdentityCardService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var identityCard = await _service.GetByIdAsync(id.Value);
            if (identityCard == null) return NotFound();
            return View(identityCard);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new IdentityCardCreateViewModel
            {
                Actors = await _context.Actors.Where(a => a.IsEnabled == true).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstFirstName + " " + a.LastFirstName }).ToListAsync(),
                IdcardTypes = await _context.IdentityCardTypes.Where(i => i.IsEnabled == true).Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).ToListAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityCardCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var identityCard = new IdentityCard
                {
                    ActorsId = viewModel.ActorsId,
                    IdcardTypesId = viewModel.IdcardTypesId,
                    Idcard = viewModel.Idcard,
                    IsEnabled = viewModel.IsEnabled
                };
                await _service.CreateAsync(identityCard);
                return RedirectToAction(nameof(Index));
            }
            
            viewModel.Actors = await _context.Actors.Where(a => a.IsEnabled == true).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstFirstName + " " + a.LastFirstName }).ToListAsync();
            viewModel.IdcardTypes = await _context.IdentityCardTypes.Where(i => i.IsEnabled == true).Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).ToListAsync();
            return View(viewModel);
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
        public async Task<IActionResult> Edit(Guid id, IdentityCard identityCard)
        {
            if (id != identityCard.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(identityCard);
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
