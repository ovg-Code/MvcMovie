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
    public class EmailsController : Controller
    {
        private readonly IEmailService _service;
        private readonly ApplicationDbContext _context;

        public EmailsController(IEmailService service, ApplicationDbContext context)
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
            var email = await _service.GetByIdAsync(id.Value);
            if (email == null) return NotFound();
            return View(email);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new EmailCreateViewModel
            {
                Actors = await _context.Actors
                    .Where(a => a.IsEnabled == true)
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstFirstName + " " + a.LastFirstName })
                    .ToListAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmailCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var email = new Email
                {
                    ActorsId = viewModel.ActorsId,
                    Definition = viewModel.Definition,
                    IsNotification = viewModel.IsNotification,
                    IsUnsuscribed = viewModel.IsUnsuscribed,
                    IsPrimary = viewModel.IsPrimary,
                    IsEnabled = viewModel.IsEnabled
                };
                await _service.CreateAsync(email);
                return RedirectToAction(nameof(Index));
            }
            
            viewModel.Actors = await _context.Actors.Where(a => a.IsEnabled == true).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstFirstName + " " + a.LastFirstName }).ToListAsync();
            return View(viewModel);
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
        public async Task<IActionResult> Edit(Guid id, Email email)
        {
            if (id != email.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(email);
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
