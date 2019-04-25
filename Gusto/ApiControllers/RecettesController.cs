using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GustoLib.Data;

namespace Gusto.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecettesController : ControllerBase
    {
        private readonly GustoDbContext _context;

        public RecettesController(GustoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lister les recettes
        /// </summary>
        /// <returns>une list de recettes</returns>
        // GET: api/Recettes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recette>>> GetRecette()
        {
            return await _context.Recette.ToListAsync();
        }

        /// <summary>
        /// Affiche une recette en fonction de l ID
        /// </summary>
        /// <param name="id">ID recherché</param>
        /// <returns>Une recette</returns>
        // GET: api/Recettes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recette>> GetRecette(int id)
        {
            var recette = await _context.Recette.FindAsync(id);

            if (recette == null)
            {
                return NotFound();
            }

            return recette;
        }

        /// <summary>
        /// Affiche des recettes en fonction de la recherche
        /// </summary>
        /// <param name="search/{name}">recherche</param>
        /// <returns>Liste de recettes </returns>

        // GET: api/Championships/search/name
        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<Recette>>> GetResearch(string name, int id)
        {
            return await _context.Recette.Where(x => x.Titre.Contains(name)).ToListAsync();
        }

        /// <summary>
        /// Modifie une recette en fonction de l ID
        /// </summary>
        /// <param name="id">ID recherché</param>
        /// <returns>Un championnat</returns>
        // PUT: api/Recettes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecette(int id, Recette recette)
        {
            if (id != recette.ID)
            {
                return BadRequest();
            }

            _context.Entry(recette).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecetteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        /// <summary>
        /// Demande la suppression d'une recette en fonction de l ID
        /// </summary>
        /// <param name="id">ID recette à supprimer</param>
        /// <returns>Une recette</returns>
        
        // DELETE: api/Recettes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Recette>> DeleteRecette(int id)
        {
            var recette = await _context.Recette.FindAsync(id);
            if (recette == null)
            {
                return NotFound();
            }

            _context.Recette.Remove(recette);
            await _context.SaveChangesAsync();

            return recette;
        }

        private bool RecetteExists(int id)
        {
            return _context.Recette.Any(e => e.ID == id);
        }
    }
}
