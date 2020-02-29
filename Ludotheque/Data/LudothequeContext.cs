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

        public DbSet<Ludotheque.Models.Game> Games { get; set; }
        public DbSet<Ludotheque.Models.Difficulty> Difficulties { get; set; }
        public DbSet<Ludotheque.Models.Editor> Editors { get; set; }
        public DbSet<Ludotheque.Models.Illustrator> Illustrators { get; set; }
        public DbSet<Ludotheque.Models.Theme> Theme { get; set; }
        public DbSet<Ludotheque.Models.ThemesGames> ThemesGames { get; set; }
        public DbSet<Ludotheque.Models.MaterialSupport> MaterialSupport { get; set; }
        public DbSet<Ludotheque.Models.MaterialSupportsGames> MaterialSupportsGames { get; set; }
        public DbSet<Ludotheque.Models.Mechanism> Mechanism { get; set; }
        public DbSet<Ludotheque.Models.MechanismsGames> MechanismsGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("Games");
            modelBuilder.Entity<Difficulty>().ToTable("Difficulties");
            modelBuilder.Entity<Editor>().ToTable("Editors");
            modelBuilder.Entity<Illustrator>().ToTable("Illustrators");
            modelBuilder.Entity<Theme>().ToTable("Themes");
            modelBuilder.Entity<ThemesGames>().ToTable("ThemesGames");
            modelBuilder.Entity<MaterialSupport>().ToTable("MaterialSupports");
            modelBuilder.Entity<MaterialSupportsGames>().ToTable("MaterialSupportsGames");
            modelBuilder.Entity<Mechanism>().ToTable("Mechanisms");
            modelBuilder.Entity<MechanismsGames>().ToTable("MechanismsGames");

            modelBuilder.Entity<ThemesGames>()
                .HasKey(c => new { c.GameId, c.ThemeId });
            modelBuilder.Entity<MaterialSupportsGames>()
                .HasKey(c => new { c.GameId, c.MaterialSupportId });
            modelBuilder.Entity<MechanismsGames>()
                .HasKey(c => new { c.GameId, c.MechanismId });
            modelBuilder.Entity<ThemesGames>()
                .HasOne(bc => bc.Theme)
                .WithMany(b => b.ThemesGames)
                .HasForeignKey(bc => bc.ThemeId);
            modelBuilder.Entity<ThemesGames>()
                .HasOne(bc => bc.Game)
                .WithMany(c => c.ThemesGames)
                .HasForeignKey(bc => bc.GameId);

            modelBuilder.Entity<MaterialSupportsGames>()
                .HasOne(bc => bc.MaterialSupport)
                .WithMany(b => b.MaterialSupportsGames)
                .HasForeignKey(bc => bc.MaterialSupportId);
            modelBuilder.Entity<MaterialSupportsGames>()
                .HasOne(bc => bc.Game)
                .WithMany(c => c.MaterialSupportsGames)
                .HasForeignKey(bc => bc.GameId);

            modelBuilder.Entity<MechanismsGames>()
                .HasOne(bc => bc.Mechanism)
                .WithMany(b => b.MechanismsGames)
                .HasForeignKey(bc => bc.MechanismId);
            modelBuilder.Entity<MechanismsGames>()
                .HasOne(bc => bc.Game)
                .WithMany(c => c.MechanismsGames)
                .HasForeignKey(bc => bc.GameId);
        }


    }
}
