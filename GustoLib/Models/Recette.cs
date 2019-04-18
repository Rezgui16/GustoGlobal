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
        public string Titre { get; set; }
        public string Description { get; set; }
        public string Difficulte { get; set; }
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
