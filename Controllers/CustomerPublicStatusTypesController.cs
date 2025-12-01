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
    public class CustomerPublicStatusTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerPublicStatusTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerPublicStatusTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerPublicStatusTypes.ToListAsync());
        }

        // GET: CustomerPublicStatusTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerPublicStatusType = await _context.CustomerPublicStatusTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerPublicStatusType == null)
            {
                return NotFound();
            }

            return View(customerPublicStatusType);
        }

        // GET: CustomerPublicStatusTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerPublicStatusTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] CustomerPublicStatusType customerPublicStatusType)
        {
            if (ModelState.IsValid)
            {
                customerPublicStatusType.Id = Guid.NewGuid();
                _context.Add(customerPublicStatusType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerPublicStatusType);
        }

        // GET: CustomerPublicStatusTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerPublicStatusType = await _context.CustomerPublicStatusTypes.FindAsync(id);
            if (customerPublicStatusType == null)
            {
                return NotFound();
            }
            return View(customerPublicStatusType);
        }

        // POST: CustomerPublicStatusTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] CustomerPublicStatusType customerPublicStatusType)
        {
            if (id != customerPublicStatusType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerPublicStatusType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerPublicStatusTypeExists(customerPublicStatusType.Id))
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
            return View(customerPublicStatusType);
        }

        // GET: CustomerPublicStatusTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerPublicStatusType = await _context.CustomerPublicStatusTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerPublicStatusType == null)
            {
                return NotFound();
            }

            return View(customerPublicStatusType);
        }

        // POST: CustomerPublicStatusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var customerPublicStatusType = await _context.CustomerPublicStatusTypes.FindAsync(id);
            if (customerPublicStatusType != null)
            {
                _context.CustomerPublicStatusTypes.Remove(customerPublicStatusType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerPublicStatusTypeExists(Guid id)
        {
            return _context.CustomerPublicStatusTypes.Any(e => e.Id == id);
        }
    }
}
