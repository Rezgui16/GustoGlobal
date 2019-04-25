using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gusto.Models;
using GustoLib.Data;

namespace Gusto.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(GustoDbContext context) : base(context)
        {

        }
        public IActionResult Index()
        {
            var vm = new HomeViewModel();
            using (var C = _context )
            {

              //expression lambda 
                vm.ListCategorie = C.Categorie.Select(p => new Categorie { Name = p.Name, ID = p.ID }).ToList();
                vm.RecetteFive = C.Recette.OrderByDescending(x => x.Moyenne).Take(3).ToList();
                vm.RecetteFacile = C.Recette.Where(p => p.Difficulte == "Facile").ToList();


            }

            return View(vm);
            
        }
        public IActionResult SaveSearch(string RechrecheRecette)
        {
            var vm = new SearchViewModel();
            using (var C= _context)
            {
                vm.SearchRecetteResult = C.Recette.Where(p => p.Titre == RechrecheRecette).ToList();
                
            }

            return View(vm);
        }

        public IActionResult CatSearch(int Categorie)
        {
            var vm = new SearchViewModel();
            using (var C = _context) {
                
                vm.SearchRecetteResult = C.Recette.Where(p => p.CategorieID == Categorie).ToList();
                    }
            return View(vm);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
