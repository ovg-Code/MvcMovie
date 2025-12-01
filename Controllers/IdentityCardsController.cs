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
    public class IdentityCardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IdentityCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IdentityCards
        public async Task<IActionResult> Index()
        {
            return View(await _context.IdentityCards.ToListAsync());
        }

        // GET: IdentityCards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityCard = await _context.IdentityCards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (identityCard == null)
            {
                return NotFound();
            }

            return View(identityCard);
        }

        // GET: IdentityCards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IdentityCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActorsId,IdcardTypesId,Idcard,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] IdentityCard identityCard)
        {
            if (ModelState.IsValid)
            {
                identityCard.Id = Guid.NewGuid();
                _context.Add(identityCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(identityCard);
        }

        // GET: IdentityCards/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityCard = await _context.IdentityCards.FindAsync(id);
            if (identityCard == null)
            {
                return NotFound();
            }
            return View(identityCard);
        }

        // POST: IdentityCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ActorsId,IdcardTypesId,Idcard,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] IdentityCard identityCard)
        {
            if (id != identityCard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(identityCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdentityCardExists(identityCard.Id))
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
            return View(identityCard);
        }

        // GET: IdentityCards/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityCard = await _context.IdentityCards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (identityCard == null)
            {
                return NotFound();
            }

            return View(identityCard);
        }

        // POST: IdentityCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var identityCard = await _context.IdentityCards.FindAsync(id);
            if (identityCard != null)
            {
                _context.IdentityCards.Remove(identityCard);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IdentityCardExists(Guid id)
        {
            return _context.IdentityCards.Any(e => e.Id == id);
        }
    }
}
