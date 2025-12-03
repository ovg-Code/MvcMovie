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
    public class AddressesController : Controller
    {
        private readonly IAddressService _service;
        private readonly ApplicationDbContext _context;

        public AddressesController(IAddressService service, ApplicationDbContext context)
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
            var address = await _service.GetByIdAsync(id.Value);
            if (address == null) return NotFound();
            return View(address);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new AddressCreateViewModel
            {
                Actors = await _context.Actors.Where(a => a.IsEnabled == true).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstFirstName + " " + a.LastFirstName }).ToListAsync(),
                AddressTypes = await _context.AddressTypes.Where(a => a.IsEnabled == true).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).ToListAsync(),
                ZipCodes = await _context.ZipCodes.Where(z => z.IsEnabled == true).Select(z => new SelectListItem { Value = z.Id.ToString(), Text = z.Name }).ToListAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddressCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var address = new Address
                {
                    ActorsId = viewModel.ActorsId,
                    AddressTypesId = viewModel.AddressTypesId,
                    ZipCodesId = viewModel.ZipCodesId,
                    Street = viewModel.Street,
                    Apartment = viewModel.Apartment,
                    Latitude = viewModel.Latitude,
                    Longitude = viewModel.Longitude,
                    IsVerified = viewModel.IsVerified,
                    IsEnabled = viewModel.IsEnabled
                };
                await _service.CreateAsync(address);
                return RedirectToAction(nameof(Index));
            }
            
            viewModel.Actors = await _context.Actors.Where(a => a.IsEnabled == true).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstFirstName + " " + a.LastFirstName }).ToListAsync();
            viewModel.AddressTypes = await _context.AddressTypes.Where(a => a.IsEnabled == true).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).ToListAsync();
            viewModel.ZipCodes = await _context.ZipCodes.Where(z => z.IsEnabled == true).Select(z => new SelectListItem { Value = z.Id.ToString(), Text = z.Name }).ToListAsync();
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var address = await _service.GetByIdAsync(id.Value);
            if (address == null) return NotFound();
            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Address address)
        {
            if (id != address.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(address);
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var address = await _service.GetByIdAsync(id.Value);
            if (address == null) return NotFound();
            return View(address);
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
