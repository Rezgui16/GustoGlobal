using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GustoLib.Data
{
    public class Recette
    {
        
        public int ID { get; set; }

        [Display(Name = "Titre", Prompt = "Fondant_Chocolat")]
        [StringLength(50)]
        [Required]
        public string Titre { get; set; }

        public string Description { get; set; }

        [Display(Name = "Difficulté", Prompt = "Fondant_Chocolat")]
        [StringLength(9)]
        [Required]
        public string Difficulte { get; set; }

        [Display(Name = "Minutes", Prompt = "60")]
        [StringLength(4)]
        [Required]
        public int TempsCuisson { get; set; }

        public float Moyenne { get; set; }
        public int  Compteur { get; set; }
        public string LienPhoto { get; set; }
        public string UserID { get; set; }
        public int CategorieID { get; set; }
        

        [ForeignKey("UserID")]
        public  User User { get; set; }
        [ForeignKey("CategorieID")]
        public  Categorie Categorie { get; set; }

        public ICollection<Favoris> Favoris { get; set; }


    }
}
