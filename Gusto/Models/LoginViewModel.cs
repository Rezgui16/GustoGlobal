using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gusto.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Adresse email", Prompt = "exemple@exemple.com")]
        //[DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} obligatoire")]
        [EmailAddress(ErrorMessage = "{0} n'est pas au bon format.")]
        public string Email { get; set; }

        [Display(Name = "Mot de passe", Prompt = "password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} obligatoire")]
        //[RegularExpression("expression", ErrorMessage = "")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "{0} doit contenir entre {2} et {1} caractères.")]
        public string Password { get; set; }

        [Display(Name = "Se souvenir de moi")]
        public bool RememberMe { get; set; }
    }
}
