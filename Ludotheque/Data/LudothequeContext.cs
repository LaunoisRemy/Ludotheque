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

    }
}
