using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    /// <summary>
    /// Controlador MVC para el catalogo de tipos de direccion.
    /// </summary>
    public class AddressTypesController : Controller
    {
        private readonly IAddressTypeService _service;

        public AddressTypesController(IAddressTypeService service)
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
            var addressType = await _service.GetByIdAsync(id.Value);
            if (addressType == null) return NotFound();
            return View(addressType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddressType addressType)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(addressType);
                return RedirectToAction(nameof(Index));
            }
            return View(addressType);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var addressType = await _service.GetByIdAsync(id.Value);
            if (addressType == null) return NotFound();
            return View(addressType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AddressType addressType)
        {
            if (id != addressType.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(addressType);
                return RedirectToAction(nameof(Index));
            }
            return View(addressType);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var addressType = await _service.GetByIdAsync(id.Value);
            if (addressType == null) return NotFound();
            return View(addressType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                TempData["SuccessMessage"] = "Tipo de Dirección eliminado exitosamente.";
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
    }
}
