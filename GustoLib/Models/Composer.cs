using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GustoLib.Data
{
    public class Composer {
        public int ID { get; set; }

        public int RecetteID { get; set; }
        public int IngredientID{ get; set; }

        public float Quantite { get; set; }

        [ForeignKey("RecetteID")]
        public  Recette Recette { get; set; }
        [ForeignKey("IngredientID")]
        public  Ingredient Ingredient { get; set; }
    }
}
