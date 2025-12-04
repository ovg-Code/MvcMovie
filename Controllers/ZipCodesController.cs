using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    /// <summary>
    /// Controlador MVC para el catalogo de codigos postales.
    /// </summary>
    public class ZipCodesController : Controller
    {
        private readonly IZipCodeService _service;
        private readonly INeighborhoodService _neighborhoodService;

        public ZipCodesController(IZipCodeService service, INeighborhoodService neighborhoodService)
        {
            _service = service;
            _neighborhoodService = neighborhoodService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var zipCode = await _service.GetByIdAsync(id.Value);
            if (zipCode == null) return NotFound();
            return View(zipCode);
        }

        public async Task<IActionResult> Create()
        {
            await LoadDropdownDataAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ZipCode zipCode)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(zipCode);
                return RedirectToAction(nameof(Index));
            }
            await LoadDropdownDataAsync();
            return View(zipCode);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var zipCode = await _service.GetByIdAsync(id.Value);
            if (zipCode == null) return NotFound();
            await LoadDropdownDataAsync();
            return View(zipCode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ZipCode zipCode)
        {
            if (id != zipCode.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(zipCode);
                return RedirectToAction(nameof(Index));
            }
            await LoadDropdownDataAsync();
            return View(zipCode);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var zipCode = await _service.GetByIdAsync(id.Value);
            if (zipCode == null) return NotFound();
            return View(zipCode);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                TempData["SuccessMessage"] = "Código Postal eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex) 
                when (ex.InnerException is Npgsql.PostgresException pgEx && pgEx.SqlState == "23503")
            {
                TempData["ErrorMessage"] = "No se puede eliminar este registro porque tiene datos relacionados. Primero debe eliminar o reasignar los registros relacionados.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al eliminar: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        private async Task LoadDropdownDataAsync()
        {
            var neighborhoods = await _neighborhoodService.GetAllAsync();
            ViewBag.Neighborhoods = new SelectList(neighborhoods, "Id", "Name");
        }
    }
}
