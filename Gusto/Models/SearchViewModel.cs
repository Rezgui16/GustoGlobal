using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GustoLib.Data;

namespace Gusto.Models
{
    public class SearchViewModel
    {   public SearchViewModel() { SearchRecetteResult = new List<Recette>(); }
        


             public List<Recette> SearchRecetteResult { get; set; }
    }

   
}
