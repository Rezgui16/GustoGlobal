using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GustoLib.Data
{
    public class Categorie {

        public int ID { get; set; }

        [Display(Name = "Name", Prompt = "Dessert")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Description", Prompt = "petites douceurs...")]
        public string Description { get; set; }
    }
}
