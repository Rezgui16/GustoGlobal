using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GustoLib.Data
{
    public class GustoDbContext: IdentityDbContext<User>
    {
        public GustoDbContext(DbContextOptions<GustoDbContext> options) : base(options) { }

        public  DbSet<Avis> Avis { get; set; }
        public  DbSet<Categorie> Categorie { get; set; }
        public  DbSet<Composer> Composer { get; set; }
        public  DbSet<Etape> Etape { get; set; }
        public  DbSet<Favoris> Favoris { get; set; }
        public  DbSet<Ingredient> Ingredient { get; set; }
        public  DbSet<Note> Note { get; set; }
        public  DbSet<Recette>Recette { get; set; }
        public  DbSet<User> User { get; set; }
        



    }
}
