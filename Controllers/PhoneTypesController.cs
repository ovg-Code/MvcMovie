using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    /// <summary>
    /// Controlador MVC para el catalogo de tipos de telefono.
    /// </summary>
    public class PhoneTypesController : Controller
    {
        private readonly IPhoneTypeService _service;

        public PhoneTypesController(IPhoneTypeService service)
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
            var phoneType = await _service.GetByIdAsync(id.Value);
            if (phoneType == null) return NotFound();
            return View(phoneType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhoneType phoneType)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(phoneType);
                return RedirectToAction(nameof(Index));
            }
            return View(phoneType);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var phoneType = await _service.GetByIdAsync(id.Value);
            if (phoneType == null) return NotFound();
            return View(phoneType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PhoneType phoneType)
        {
            if (id != phoneType.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(phoneType);
                return RedirectToAction(nameof(Index));
            }
            return View(phoneType);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var phoneType = await _service.GetByIdAsync(id.Value);
            if (phoneType == null) return NotFound();
            return View(phoneType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                TempData["SuccessMessage"] = "Tipo de Teléfono eliminado exitosamente.";
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
