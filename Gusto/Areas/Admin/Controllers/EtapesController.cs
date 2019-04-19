using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GustoLib.Data;

namespace Gusto.Areas.Admin.Controllers
{
    public class EtapesController : BaseAdminController
    {
        public EtapesController(GustoDbContext context) : base(context)
        {
        }

        // GET: Admin/Etapes
        public async Task<IActionResult> Index()
        {
            var gustoDbContext = _context.Etape.Include(e => e.Recette);
            return View(await gustoDbContext.ToListAsync());
        }

        // GET: Admin/Etapes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etape = await _context.Etape
                .Include(e => e.Recette)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (etape == null)
            {
                return NotFound();
            }

            return View(etape);
        }

        // GET: Admin/Etapes/Create
        public IActionResult Create()
        {
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID");
            return View();
        }

        // POST: Admin/Etapes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Instruction,Nom,RecetteID")] Etape etape)
        {
            if (ModelState.IsValid)
            {
                _context.Add(etape);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID", etape.RecetteID);
            return View(etape);
        }

        // GET: Admin/Etapes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etape = await _context.Etape.FindAsync(id);
            if (etape == null)
            {
                return NotFound();
            }
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID", etape.RecetteID);
            return View(etape);
        }

        // POST: Admin/Etapes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Instruction,Nom,RecetteID")] Etape etape)
        {
            if (id != etape.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(etape);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtapeExists(etape.ID))
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
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID", etape.RecetteID);
            return View(etape);
        }

        // GET: Admin/Etapes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etape = await _context.Etape
                .Include(e => e.Recette)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (etape == null)
            {
                return NotFound();
            }

            return View(etape);
        }

        // POST: Admin/Etapes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var etape = await _context.Etape.FindAsync(id);
            _context.Etape.Remove(etape);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EtapeExists(int id)
        {
            return _context.Etape.Any(e => e.ID == id);
        }
    }
}
