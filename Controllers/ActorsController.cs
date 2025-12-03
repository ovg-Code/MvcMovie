using Microsoft.AspNetCore.Mvc;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    /// <summary>
    /// Controlador MVC para la gestion de actores.
    /// </summary>
    public class ActorsController : Controller
    {
        private readonly IActorService _service;

        public ActorsController(IActorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var actors = await _service.GetAllAsync();
            return View(actors);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            
            var actor = await _service.GetByIdAsync(id.Value);
            if (actor == null) return NotFound();
            
            return View(actor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActorTypesId,GendersId,NationalityCountriesId,Title,Prefix,Suffix,IsPep,FirstFirstName,SecondFirstName,LastFirstName,LastSecondName,BirthdayAt,OtherData,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(actor);
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            
            var actor = await _service.GetByIdAsync(id.Value);
            if (actor == null) return NotFound();
            
            return View(actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ActorTypesId,GendersId,NationalityCountriesId,Title,Prefix,Suffix,IsPep,FirstFirstName,SecondFirstName,LastFirstName,LastSecondName,BirthdayAt,OtherData,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Actor actor)
        {
            if (id != actor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(actor);
                }
                catch (Exception)
                {
                    if (!await _service.ExistsAsync(actor.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            
            var actor = await _service.GetByIdAsync(id.Value);
            if (actor == null) return NotFound();
            
            return View(actor);
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
