using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    public class ThemesGames
    {
        [Key]
        public int GameId { get; set; }
        [Key]
        public int ThemeId { get; set; }

        public Game Game { get; set; }
        public Theme Theme { get; set; }
    }
}
