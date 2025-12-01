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
    public class ActorTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ActorTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ActorTypes.ToListAsync());
        }

        // GET: ActorTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorType = await _context.ActorTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actorType == null)
            {
                return NotFound();
            }

            return View(actorType);
        }

        // GET: ActorTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActorTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] ActorType actorType)
        {
            if (ModelState.IsValid)
            {
                actorType.Id = Guid.NewGuid();
                _context.Add(actorType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actorType);
        }

        // GET: ActorTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorType = await _context.ActorTypes.FindAsync(id);
            if (actorType == null)
            {
                return NotFound();
            }
            return View(actorType);
        }

        // POST: ActorTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] ActorType actorType)
        {
            if (id != actorType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actorType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorTypeExists(actorType.Id))
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
            return View(actorType);
        }

        // GET: ActorTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorType = await _context.ActorTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actorType == null)
            {
                return NotFound();
            }

            return View(actorType);
        }

        // POST: ActorTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var actorType = await _context.ActorTypes.FindAsync(id);
            if (actorType != null)
            {
                _context.ActorTypes.Remove(actorType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorTypeExists(Guid id)
        {
            return _context.ActorTypes.Any(e => e.Id == id);
        }
    }
}
