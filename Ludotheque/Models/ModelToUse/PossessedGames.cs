using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    public class PossessedGames
    {
        public Game Game { get; set; }
        public bool Own { get; set; }
    }
}
