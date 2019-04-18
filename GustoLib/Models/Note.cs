using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GustoLib.Data
{
    public class Note
    {
        
        public int ID { get; set; }
        public int Notation { get; set; }
        public string UserID { get; set; }
        public int RecetteID{ get; set; }

        [ForeignKey("RecetteID")]
        public Recette Recette { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
