using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GustoLib.Data;

namespace Gusto.Models
{
    public class RecetteViewModel
    {
        public List<Etape> ListEtape { get;  set; }
        
        public Recette recette { get; set; }
        public List<Composer> ListComposent { get; set; }
        public List<Ingredient> ListIngredient { get; set;}

        public RecetteViewModel()
        {   
            ListEtape = new List<Etape>();
            
            ListComposent = new List<Composer>();
            ListIngredient = new List<Ingredient>();
                }
       
    }
}
