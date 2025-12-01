using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Controllers
{
    public class MunicipalitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MunicipalitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Municipalitys
        public async Task<IActionResult> Index()
        {
            return View(await _context.Municipalities.ToListAsync());
        }

        // GET: Municipalitys/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipality = await _context.Municipalities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (municipality == null)
            {
                return NotFound();
            }

            return View(municipality);
        }

        // GET: Municipalitys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Municipalitys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CitiesId,Name,Code,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Municipality municipality)
        {
            if (ModelState.IsValid)
            {
                municipality.Id = Guid.NewGuid();
                _context.Add(municipality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(municipality);
        }

        // GET: Municipalitys/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipality = await _context.Municipalities.FindAsync(id);
            if (municipality == null)
            {
                return NotFound();
            }
            return View(municipality);
        }

        // POST: Municipalitys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CitiesId,Name,Code,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] Municipality municipality)
        {
            if (id != municipality.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(municipality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MunicipalityExists(municipality.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(municipality);
        }

        // GET: Municipalitys/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipality = await _context.Municipalities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (municipality == null)
            {
                return NotFound();
            }

            return View(municipality);
        }

        // POST: Municipalitys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var municipality = await _context.Municipalities.FindAsync(id);
            if (municipality != null)
            {
                _context.Municipalities.Remove(municipality);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MunicipalityExists(Guid id)
        {
            return _context.Municipalities.Any(e => e.Id == id);
        }
    }
}
