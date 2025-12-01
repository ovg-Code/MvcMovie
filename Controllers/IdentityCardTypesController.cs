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
    public class IdentityCardTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IdentityCardTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IdentityCardTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.IdentityCardTypes.ToListAsync());
        }

        // GET: IdentityCardTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityCardType = await _context.IdentityCardTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (identityCardType == null)
            {
                return NotFound();
            }

            return View(identityCardType);
        }

        // GET: IdentityCardTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IdentityCardTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] IdentityCardType identityCardType)
        {
            if (ModelState.IsValid)
            {
                identityCardType.Id = Guid.NewGuid();
                _context.Add(identityCardType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(identityCardType);
        }

        // GET: IdentityCardTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityCardType = await _context.IdentityCardTypes.FindAsync(id);
            if (identityCardType == null)
            {
                return NotFound();
            }
            return View(identityCardType);
        }

        // POST: IdentityCardTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] IdentityCardType identityCardType)
        {
            if (id != identityCardType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(identityCardType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdentityCardTypeExists(identityCardType.Id))
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
            return View(identityCardType);
        }

        // GET: IdentityCardTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityCardType = await _context.IdentityCardTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (identityCardType == null)
            {
                return NotFound();
            }

            return View(identityCardType);
        }

        // POST: IdentityCardTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var identityCardType = await _context.IdentityCardTypes.FindAsync(id);
            if (identityCardType != null)
            {
                _context.IdentityCardTypes.Remove(identityCardType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IdentityCardTypeExists(Guid id)
        {
            return _context.IdentityCardTypes.Any(e => e.Id == id);
        }
    }
}
