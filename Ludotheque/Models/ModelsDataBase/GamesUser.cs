using Ludotheque.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    public class GamesUser
    {
        [Key]
        public int GameId { get; set; }
        [Key]
        public string LudothequeUserId { get; set; }

        public Game Game { get; set; }
        public LudothequeUser User { get; set; }
    }
}
