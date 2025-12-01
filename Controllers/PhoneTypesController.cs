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
    public class PhoneTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhoneTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PhoneTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhoneTypes.ToListAsync());
        }

        // GET: PhoneTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneType = await _context.PhoneTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phoneType == null)
            {
                return NotFound();
            }

            return View(phoneType);
        }

        // GET: PhoneTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhoneTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] PhoneType phoneType)
        {
            if (ModelState.IsValid)
            {
                phoneType.Id = Guid.NewGuid();
                _context.Add(phoneType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phoneType);
        }

        // GET: PhoneTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneType = await _context.PhoneTypes.FindAsync(id);
            if (phoneType == null)
            {
                return NotFound();
            }
            return View(phoneType);
        }

        // POST: PhoneTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] PhoneType phoneType)
        {
            if (id != phoneType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phoneType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneTypeExists(phoneType.Id))
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
            return View(phoneType);
        }

        // GET: PhoneTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneType = await _context.PhoneTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phoneType == null)
            {
                return NotFound();
            }

            return View(phoneType);
        }

        // POST: PhoneTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var phoneType = await _context.PhoneTypes.FindAsync(id);
            if (phoneType != null)
            {
                _context.PhoneTypes.Remove(phoneType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneTypeExists(Guid id)
        {
            return _context.PhoneTypes.Any(e => e.Id == id);
        }
    }
}
