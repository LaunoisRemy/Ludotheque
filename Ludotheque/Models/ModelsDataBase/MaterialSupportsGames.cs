using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    public class MaterialSupportsGames
    {
        [Key]
        public int GameId { get; set; }
        [Key]
        public int MaterialSupportId { get; set; }

        public Game Game { get; set; }
        public MaterialSupport MaterialSupport { get; set; }

    }
}
