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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Favoris>()
                .HasKey(t => new { t.RecetteID, t.UserID });

            modelBuilder.Entity<Favoris>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.Favoris)
                .HasForeignKey(pt => pt.UserID);

            modelBuilder.Entity<Favoris>()
                .HasOne(pt => pt.Recette)
                .WithMany(t => t.Favoris)
                .HasForeignKey(pt => pt.RecetteID);
        }


    }
}
