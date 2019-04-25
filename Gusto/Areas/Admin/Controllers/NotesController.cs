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
    public class NotesController : BaseAdminController
    {
        public NotesController(GustoDbContext context) : base(context)
        {
        }

        // GET: Admin/Notes
        public async Task<IActionResult> Index()
        {
            var gustoDbContext = _context.Note.Include(n => n.Recette).Include(n => n.User);
            return View(await gustoDbContext.ToListAsync());
        }

        // GET: Admin/Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .Include(n => n.Recette)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // GET: Admin/Notes/Create
        public IActionResult Create()
        {
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID");
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: Admin/Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Notation,UserID,RecetteID")] Note note)
        {
            if (ModelState.IsValid)
            {
                _context.Add(note);
                await _context.SaveChangesAsync();
                DisplayMessage("La note a bien été prise en compte", Class.TypeMessage.SUCCESS);
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID", note.RecetteID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", note.UserID);
            DisplayMessage("La note n'est pas valide", Class.TypeMessage.DANGER);
            return View(note);
        }

        // GET: Admin/Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID", note.RecetteID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", note.UserID);
            return View(note);
        }

        // POST: Admin/Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Notation,UserID,RecetteID")] Note note)
        {
            if (id != note.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.ID))
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
            ViewData["RecetteID"] = new SelectList(_context.Recette, "ID", "ID", note.RecetteID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", note.UserID);
            DisplayMessage("Votre note sur cette recette a bien été modifiée", Class.TypeMessage.SUCCESS);
            return View(note);
        }

        // GET: Admin/Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .Include(n => n.Recette)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Admin/Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Note.FindAsync(id);
            _context.Note.Remove(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.ID == id);
        }
    }
}
