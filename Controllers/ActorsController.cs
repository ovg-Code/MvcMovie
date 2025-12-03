using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ari2._0.Models;
using ari2._0.Services;

namespace ari2._0.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorService _service;
        private readonly IActorTypeService _actorTypeService;
        private readonly IGenderService _genderService;
        private readonly ICountryService _countryService;

        public ActorsController(
            IActorService service,
            IActorTypeService actorTypeService,
            IGenderService genderService,
            ICountryService countryService)
        {
            _service = service;
            _actorTypeService = actorTypeService;
            _genderService = genderService;
            _countryService = countryService;
        }

        public async Task<IActionResult> Index()
        {
            var actors = await _service.GetAllWithRelationsAsync();
            return View(actors);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            
            var actor = await _service.GetByIdAsync(id.Value);
            if (actor == null) return NotFound();
            
            return View(actor);
        }

        public async Task<IActionResult> Create()
        {
            await LoadDropdownDataAsync();
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
            await LoadDropdownDataAsync();
            return View(actor);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            
            var actor = await _service.GetByIdAsync(id.Value);
            if (actor == null) return NotFound();
            
            await LoadDropdownDataAsync();
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
            await LoadDropdownDataAsync();
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

        private async Task LoadDropdownDataAsync()
        {
            var actorTypes = await _actorTypeService.GetAllAsync();
            var genders = await _genderService.GetAllAsync();
            var countries = await _countryService.GetAllAsync();

            ViewBag.ActorTypes = new SelectList(actorTypes, "Id", "Name");
            ViewBag.Genders = new SelectList(genders, "Id", "Name");
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
        }
    }
}
