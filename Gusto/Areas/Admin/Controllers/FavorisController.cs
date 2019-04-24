using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GustoLib.Data;
using Microsoft.AspNetCore.Authorization;

namespace Gusto.Areas.Admin.Controllers
{
    [Authorize]
    public class FavorisController : BaseAdminController
    {
        public FavorisController(GustoDbContext context) : base(context)
        {
        }

        // GET: Admin/Favoris
        public async Task<IActionResult> Index()
        {
            var gustoDbContext = _context.Favoris.Include(f => f.Recette).Include(f => f.User);
            return View(await gustoDbContext.ToListAsync());
        }

        // GET: Admin/Favoris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoris = await _context.Favoris
                .Include(f => f.Recette)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.RecetteID == id);
            if (favoris == null)
            {
                return NotFound();
            }

            return View(favoris);
        }

        // GET: Admin/Favoris/Create
        public IActionResult Create()
        {
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID");
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: Admin/Favoris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,RecetteID")] Favoris favoris)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favoris);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID", favoris.RecetteID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", favoris.UserID);
            return View(favoris);
        }

        // GET: Admin/Favoris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoris = await _context.Favoris.FindAsync(id);
            if (favoris == null)
            {
                return NotFound();
            }
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID", favoris.RecetteID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", favoris.UserID);
            return View(favoris);
        }

        // POST: Admin/Favoris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,RecetteID")] Favoris favoris)
        {
            if (id != favoris.RecetteID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favoris);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavorisExists(favoris.RecetteID))
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
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID", favoris.RecetteID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", favoris.UserID);
            return View(favoris);
        }

        // GET: Admin/Favoris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoris = await _context.Favoris
                .Include(f => f.Recette)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.RecetteID == id);
            if (favoris == null)
            {
                return NotFound();
            }

            return View(favoris);
        }

        // POST: Admin/Favoris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favoris = await _context.Favoris.FindAsync(id);
            _context.Favoris.Remove(favoris);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavorisExists(int id)
        {
            return _context.Favoris.Any(e => e.RecetteID == id);
        }
    }
}
