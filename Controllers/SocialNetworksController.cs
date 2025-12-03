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
    public class SocialNetworksController : Controller
    {
        private readonly ISocialNetworkService _service;
        private readonly ApplicationDbContext _context;

        public SocialNetworksController(ISocialNetworkService service, ApplicationDbContext context)
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
            var socialNetwork = await _service.GetByIdAsync(id.Value);
            if (socialNetwork == null) return NotFound();
            return View(socialNetwork);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new SocialNetworkCreateViewModel
            {
                Actors = await _context.Actors.Where(a => a.IsEnabled == true).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstFirstName + " " + a.LastFirstName }).ToListAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialNetworkCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var socialNetwork = new SocialNetwork
                {
                    ActorsId = viewModel.ActorsId,
                    Platform = viewModel.Platform,
                    ProfileName = viewModel.ProfileName,
                    ProfileUrl = viewModel.ProfileUrl,
                    IsEnabled = viewModel.IsEnabled
                };
                await _service.CreateAsync(socialNetwork);
                return RedirectToAction(nameof(Index));
            }
            
            viewModel.Actors = await _context.Actors.Where(a => a.IsEnabled == true).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstFirstName + " " + a.LastFirstName }).ToListAsync();
            return View(viewModel);
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
        public async Task<IActionResult> Edit(Guid id, SocialNetwork socialNetwork)
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
