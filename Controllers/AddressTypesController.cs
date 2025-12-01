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
    public class AddressTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddressTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AddressTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AddressTypes.ToListAsync());
        }

        // GET: AddressTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressType = await _context.AddressTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addressType == null)
            {
                return NotFound();
            }

            return View(addressType);
        }

        // GET: AddressTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddressTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] AddressType addressType)
        {
            if (ModelState.IsValid)
            {
                addressType.Id = Guid.NewGuid();
                _context.Add(addressType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addressType);
        }

        // GET: AddressTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressType = await _context.AddressTypes.FindAsync(id);
            if (addressType == null)
            {
                return NotFound();
            }
            return View(addressType);
        }

        // POST: AddressTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] AddressType addressType)
        {
            if (id != addressType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addressType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressTypeExists(addressType.Id))
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
            return View(addressType);
        }

        // GET: AddressTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressType = await _context.AddressTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addressType == null)
            {
                return NotFound();
            }

            return View(addressType);
        }

        // POST: AddressTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var addressType = await _context.AddressTypes.FindAsync(id);
            if (addressType != null)
            {
                _context.AddressTypes.Remove(addressType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressTypeExists(Guid id)
        {
            return _context.AddressTypes.Any(e => e.Id == id);
        }
    }
}
