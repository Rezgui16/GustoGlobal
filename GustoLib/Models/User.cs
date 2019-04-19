using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GustoLib.Data
{
    public class User: IdentityUser
    {
        [Display(Name = "LastName", Prompt = "Rezgui")]
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "FirstName", Prompt = "Soumia")]
        [StringLength(50)]
        [Required]
        public string FirstMidName { get; set; }
        [Required]
        public DateTime DateNaissance { get; set; }
        
        public string Status { get; set; }
        
        public DateTime DateInscription { get; set; }
       
        public ICollection<Favoris> Favoris { get; set; }
    }
}
