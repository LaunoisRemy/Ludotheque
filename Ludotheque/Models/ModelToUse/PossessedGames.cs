using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    /// <summary>
    /// Class to know if this games is own by user
    /// </summary>
    public class PossessedGames
    {
        public Game Game { get; set; }
        public bool Own { get; set; }
    }
}
