using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    public class MechanismsGames
    {
        [Key]
        public int GameId { get; set; }
        [Key]
        public int MechanismId { get; set; }

        public Game Game { get; set; }
        public Mechanism Mechanism { get; set; }

    }
}
