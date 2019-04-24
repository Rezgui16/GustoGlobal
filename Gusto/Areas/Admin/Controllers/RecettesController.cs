using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GustoLib.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Gusto.Controllers;
using System.Security.Claims;

namespace Gusto.Areas.Admin.Controllers
{
    public class RecettesController : BaseAdminController
    {
        public RecettesController(GustoDbContext context) : base(context)
        {
        }

        // GET: Admin/Recettes
        public async Task<IActionResult> Index()
        {
            var gustoDbContext = _context.Recette.Include(r => r.Categorie).Include(r => r.User);
            return View(await gustoDbContext.ToListAsync());
        }
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> ListByUserId()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);            
            var gustoDbContext = _context.Recette.Include(r => r.Categorie).Where(r => r.User.Id == userId);
            return View(await gustoDbContext.ToListAsync());
        }

        // GET: Admin/Recettes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recette = await _context.Recette
                .Include(r => r.Categorie)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recette == null)
            {
                return NotFound();
            }

            return View(recette);
        }

        // GET: Admin/Recettes/Create
        [Authorize(Roles="Chef,Admin")]
        public IActionResult Create()
        {
            ViewData["CategorieID"] = new SelectList(_context.Categorie, "ID", "ID");
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: Admin/Recettes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Chef,Admin")]
        public async Task<IActionResult> Create([Bind("ID,Titre,Description,Difficulte,TempsCuisson,Moyenne,Compteur,LienPhoto,UserID,CategorieID")] Recette recette)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recette);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieID"] = new SelectList(_context.Categorie, "ID", "ID", recette.CategorieID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", recette.UserID);
            return View(recette);
        }

        // GET: Admin/Recettes/Edit/5
        [Authorize(Roles = "Chef,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recette = await _context.Recette.FindAsync(id);
            if (recette == null)
            {
                return NotFound();
            }
            ViewData["CategorieID"] = new SelectList(_context.Categorie, "ID", "ID", recette.CategorieID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", recette.UserID);
            return View(recette);
        }

        // POST: Admin/Recettes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Chef,Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Titre,Description,Difficulte,TempsCuisson,Moyenne,Compteur,LienPhoto,UserID,CategorieID")] Recette recette)
        {
            if (id != recette.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recette);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecetteExists(recette.ID))
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
            ViewData["CategorieID"] = new SelectList(_context.Categorie, "ID", "ID", recette.CategorieID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", recette.UserID);
            return View(recette);
        }

        // GET: Admin/Recettes/Delete/5
        [Authorize(Roles = "Chef,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recette = await _context.Recette
                .Include(r => r.Categorie)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recette == null)
            {
                return NotFound();
            }

            return View(recette);
        }

        // POST: Admin/Recettes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Chef,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recette = await _context.Recette.FindAsync(id);
            _context.Recette.Remove(recette);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecetteExists(int id)
        {
            return _context.Recette.Any(e => e.ID == id);
        }
    }
}
