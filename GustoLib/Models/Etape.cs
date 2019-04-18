using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GustoLib.Data
{
    public class Etape
    {
        
        public int ID { get; set; }
        public string Instruction { get; set; }
        public string Nom { get; set; }
        public int RecetteID{ get; set; }

        [ForeignKey("RecetteID")]
        public Recette Recette { get; set; }
    }
}
