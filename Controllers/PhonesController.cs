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
    public class PhonesController : Controller
    {
        private readonly IPhoneService _service;
        private readonly ApplicationDbContext _context;

        public PhonesController(IPhoneService service, ApplicationDbContext context)
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
            var phone = await _service.GetByIdAsync(id.Value);
            if (phone == null) return NotFound();
            return View(phone);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new PhoneCreateViewModel
            {
                Actors = await _context.Actors
                    .Where(a => a.IsEnabled == true)
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstFirstName + " " + a.LastFirstName })
                    .ToListAsync(),
                PhoneTypes = await _context.PhoneTypes
                    .Where(p => p.IsEnabled == true)
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                    .ToListAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhoneCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var phone = new Phone
                {
                    ActorsId = viewModel.ActorsId,
                    PhoneTypesId = viewModel.PhoneTypesId,
                    Number = viewModel.Number,
                    Extension = viewModel.Extension,
                    IsVerified = viewModel.IsVerified,
                    IsEnabled = viewModel.IsEnabled
                };
                await _service.CreateAsync(phone);
                return RedirectToAction(nameof(Index));
            }
            
            viewModel.Actors = await _context.Actors.Where(a => a.IsEnabled == true).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstFirstName + " " + a.LastFirstName }).ToListAsync();
            viewModel.PhoneTypes = await _context.PhoneTypes.Where(p => p.IsEnabled == true).Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToListAsync();
            return View(viewModel);
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
        public async Task<IActionResult> Edit(Guid id, Phone phone)
        {
            if (id != phone.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(phone);
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
