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
    public class ZipCodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZipCodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ZipCodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ZipCodes.ToListAsync());
        }

        // GET: ZipCodes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zipCode = await _context.ZipCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zipCode == null)
            {
                return NotFound();
            }

            return View(zipCode);
        }

        // GET: ZipCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ZipCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NeighborhoodsId,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] ZipCode zipCode)
        {
            if (ModelState.IsValid)
            {
                zipCode.Id = Guid.NewGuid();
                _context.Add(zipCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zipCode);
        }

        // GET: ZipCodes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zipCode = await _context.ZipCodes.FindAsync(id);
            if (zipCode == null)
            {
                return NotFound();
            }
            return View(zipCode);
        }

        // POST: ZipCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NeighborhoodsId,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] ZipCode zipCode)
        {
            if (id != zipCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zipCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZipCodeExists(zipCode.Id))
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
            return View(zipCode);
        }

        // GET: ZipCodes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zipCode = await _context.ZipCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zipCode == null)
            {
                return NotFound();
            }

            return View(zipCode);
        }

        // POST: ZipCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var zipCode = await _context.ZipCodes.FindAsync(id);
            if (zipCode != null)
            {
                _context.ZipCodes.Remove(zipCode);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZipCodeExists(Guid id)
        {
            return _context.ZipCodes.Any(e => e.Id == id);
        }
    }
}
