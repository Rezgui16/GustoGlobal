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
    public class AvisController : BaseAdminController
    {
        public AvisController(GustoDbContext context) : base(context)
        {
        }
        // GET: Admin/Avis
        public async Task<IActionResult> Index()
        {
            var gustoDbContext = _context.Avis.Include(a => a.Recette).Include(a => a.User);
            return View(await gustoDbContext.ToListAsync());
        }

        // GET: Admin/Avis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avis = await _context.Avis
                .Include(a => a.Recette)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (avis == null)
            {
                return NotFound();
            }

            return View(avis);
        }

        // GET: Admin/Avis/Create
        public IActionResult Create()
        {
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID");
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: Admin/Avis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Texte,UserID,RecetteID")] Avis avis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avis);
                await _context.SaveChangesAsync();
                DisplayMessage("Avis créé", Class.TypeMessage.SUCCESS);
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID", avis.RecetteID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", avis.UserID);            
            DisplayMessage("Avis non valide", Class.TypeMessage.DANGER);
            return View(avis);
        }

        // GET: Admin/Avis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avis = await _context.Avis.FindAsync(id);
            if (avis == null)
            {
                return NotFound();
            }
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID", avis.RecetteID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", avis.UserID);            
            return View(avis);
        }

        // POST: Admin/Avis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Texte,UserID,RecetteID")] Avis avis)
        {
            if (id != avis.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvisExists(avis.ID))
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
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID", avis.RecetteID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", avis.UserID);
            DisplayMessage("Votre avis a bien été modifié", Class.TypeMessage.SUCCESS);
            return View(avis);
        }

        // GET: Admin/Avis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avis = await _context.Avis
                .Include(a => a.Recette)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (avis == null)
            {
                return NotFound();
            }

            return View(avis);
        }

        // POST: Admin/Avis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avis = await _context.Avis.FindAsync(id);
            _context.Avis.Remove(avis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvisExists(int id)
        {
            return _context.Avis.Any(e => e.ID == id);
        }
    }
}
