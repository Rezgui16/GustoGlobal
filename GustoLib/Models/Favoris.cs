using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GustoLib.Data
{
    public class Favoris
    {
        
        public string UserID { get; set; }
        
     
        public User User { get; set; }

     
        public int RecetteID { get; set; }

    
        public Recette Recette { get; set; }

      
        


    }
}
