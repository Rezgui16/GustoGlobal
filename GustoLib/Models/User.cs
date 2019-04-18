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
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime DateNaissance { get; set; }

        public string Pseudo { get; set; }
        public string Status { get; set; }
        public DateTime DateInscription { get; set; }

    }
}
