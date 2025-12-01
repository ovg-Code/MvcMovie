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
    public class SocialNetworksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SocialNetworksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SocialNetworks
        public async Task<IActionResult> Index()
        {
            return View(await _context.SocialNetworks.ToListAsync());
        }

        // GET: SocialNetworks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socialNetwork = await _context.SocialNetworks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socialNetwork == null)
            {
                return NotFound();
            }

            return View(socialNetwork);
        }

        // GET: SocialNetworks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SocialNetworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActorsId,Platform,ProfileName,ProfileUrl,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] SocialNetwork socialNetwork)
        {
            if (ModelState.IsValid)
            {
                socialNetwork.Id = Guid.NewGuid();
                _context.Add(socialNetwork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(socialNetwork);
        }

        // GET: SocialNetworks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socialNetwork = await _context.SocialNetworks.FindAsync(id);
            if (socialNetwork == null)
            {
                return NotFound();
            }
            return View(socialNetwork);
        }

        // POST: SocialNetworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ActorsId,Platform,ProfileName,ProfileUrl,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsEnabled")] SocialNetwork socialNetwork)
        {
            if (id != socialNetwork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socialNetwork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocialNetworkExists(socialNetwork.Id))
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
            return View(socialNetwork);
        }

        // GET: SocialNetworks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socialNetwork = await _context.SocialNetworks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socialNetwork == null)
            {
                return NotFound();
            }

            return View(socialNetwork);
        }

        // POST: SocialNetworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var socialNetwork = await _context.SocialNetworks.FindAsync(id);
            if (socialNetwork != null)
            {
                _context.SocialNetworks.Remove(socialNetwork);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocialNetworkExists(Guid id)
        {
            return _context.SocialNetworks.Any(e => e.Id == id);
        }
    }
}
