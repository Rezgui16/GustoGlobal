using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GustoLib.Data
{
    public class Ingredient
    {  
        public int ID { get; set; }


        [Display(Name = "Ingrédient", Prompt = "Chocolat noir")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }


        [Display(Name = "Unité", Prompt = "grammes")]
        [StringLength(10)]
        [Required]
        public string Unite { get; set; }
        


    }
}
