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
    }
}
