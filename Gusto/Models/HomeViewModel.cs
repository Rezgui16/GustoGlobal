using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GustoLib.Data;

namespace Gusto.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()

        {
            ListCategorie = new List<Categorie>();
            RecetteFive = new List<Recette>();
            RecetteFacile= new List<Recette>();
        }

        public List<Categorie> ListCategorie { get; set; }
        public List<Recette> RecetteFive { get; set; }
        public List<Recette> RecetteFacile { get; set; }
    }
}
