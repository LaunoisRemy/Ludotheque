using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    [Obsolete("Class useless")]
    public class GameAllData
    {
        public Game Game { get; set; }
        public IEnumerable<Theme> Themes { get; set; }

        public IEnumerable<MaterialSupport> MaterialSupports { get; set; }
        public IEnumerable<Mechanism> Mechanisms { get; set; }
    }
}
