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
    public class ComposersController : BaseAdminController
    {
        public ComposersController(GustoDbContext context) : base(context)
        {
        }

        // GET: Admin/Composers
        public async Task<IActionResult> Index()
        {
            var gustoDbContext = _context.Composer.Include(c => c.Ingredient).Include(c => c.Recette);
            return View(await gustoDbContext.ToListAsync());
        }

        // GET: Admin/Composers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var composer = await _context.Composer
                .Include(c => c.Ingredient)
                .Include(c => c.Recette)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (composer == null)
            {
                return NotFound();
            }

            return View(composer);
        }

        // GET: Admin/Composers/Create
        public IActionResult Create()
        {
            ViewData["IngredientID"] = new SelectList(_context.Ingredient, "ID", "ID");
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID");
            return View();
        }

        // POST: Admin/Composers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RecetteID,IngredientID,Quantite")] Composer composer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(composer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IngredientID"] = new SelectList(_context.Ingredient, "ID", "ID", composer.IngredientID);
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID", composer.RecetteID);
            return View(composer);
        }

        // GET: Admin/Composers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var composer = await _context.Composer.FindAsync(id);
            if (composer == null)
            {
                return NotFound();
            }
            ViewData["IngredientID"] = new SelectList(_context.Ingredient, "ID", "ID", composer.IngredientID);
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID", composer.RecetteID);
            return View(composer);
        }

        // POST: Admin/Composers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RecetteID,IngredientID,Quantite")] Composer composer)
        {
            if (id != composer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(composer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComposerExists(composer.ID))
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
            ViewData["IngredientID"] = new SelectList(_context.Ingredient, "ID", "ID", composer.IngredientID);
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID", composer.RecetteID);
            return View(composer);
        }

        // GET: Admin/Composers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var composer = await _context.Composer
                .Include(c => c.Ingredient)
                .Include(c => c.Recette)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (composer == null)
            {
                return NotFound();
            }

            return View(composer);
        }

        // POST: Admin/Composers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var composer = await _context.Composer.FindAsync(id);
            _context.Composer.Remove(composer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComposerExists(int id)
        {
            return _context.Composer.Any(e => e.ID == id);
        }
    }
}
