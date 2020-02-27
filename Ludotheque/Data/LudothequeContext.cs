using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ludotheque.Models;

namespace Ludotheque.Data
{
    public class LudothequeContext : DbContext
    {
        public LudothequeContext (DbContextOptions<LudothequeContext> options)
            : base(options)
        {
        }

        public DbSet<Ludotheque.Models.Jeu> Jeu { get; set; }
        public DbSet<Ludotheque.Models.Game> Games { get; set; }
        public DbSet<Ludotheque.Models.Category> Categories { get; set; }
        public DbSet<Ludotheque.Models.Difficulty> Difficulties { get; set; }
        public DbSet<Ludotheque.Models.Editor> Editors { get; set; }
        public DbSet<Ludotheque.Models.Illustrator> Illustrators { get; set; }
        public DbSet<Ludotheque.Models.GameCategory> GameCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jeu>().ToTable("Jeu");
            modelBuilder.Entity<Game>().ToTable("Games");
            modelBuilder.Entity<Category>().ToTable("Gategories");
            modelBuilder.Entity<Difficulty>().ToTable("Difficulties");
            modelBuilder.Entity<Editor>().ToTable("Editors");
            modelBuilder.Entity<Illustrator>().ToTable("Illustrators");
            modelBuilder.Entity<GameCategory>().ToTable("GameCategories");

            modelBuilder.Entity<GameCategory>()
                .HasKey(c => new { c.CategoryId, c.GameId });
        }

    }
}
