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
    public class ActorRelationshipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorRelationshipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ActorRelationships
        public async Task<IActionResult> Index()
        {
            return View(await _context.ActorRelationships.ToListAsync());
        }

        // GET: ActorRelationships/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorRelationship = await _context.ActorRelationships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actorRelationship == null)
            {
                return NotFound();
            }

            return View(actorRelationship);
        }

        // GET: ActorRelationships/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActorRelationships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentId,ChildId,RelationshipTypesId,IsPercentage,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] ActorRelationship actorRelationship)
        {
            if (ModelState.IsValid)
            {
                actorRelationship.Id = Guid.NewGuid();
                _context.Add(actorRelationship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actorRelationship);
        }

        // GET: ActorRelationships/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorRelationship = await _context.ActorRelationships.FindAsync(id);
            if (actorRelationship == null)
            {
                return NotFound();
            }
            return View(actorRelationship);
        }

        // POST: ActorRelationships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ParentId,ChildId,RelationshipTypesId,IsPercentage,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] ActorRelationship actorRelationship)
        {
            if (id != actorRelationship.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actorRelationship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorRelationshipExists(actorRelationship.Id))
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
            return View(actorRelationship);
        }

        // GET: ActorRelationships/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorRelationship = await _context.ActorRelationships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actorRelationship == null)
            {
                return NotFound();
            }

            return View(actorRelationship);
        }

        // POST: ActorRelationships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var actorRelationship = await _context.ActorRelationships.FindAsync(id);
            if (actorRelationship != null)
            {
                _context.ActorRelationships.Remove(actorRelationship);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorRelationshipExists(Guid id)
        {
            return _context.ActorRelationships.Any(e => e.Id == id);
        }
    }
}
