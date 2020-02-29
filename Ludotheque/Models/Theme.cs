using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    public class Theme
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nom")]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ThemesGames> ThemesGames { get; set; }

        public Game Game { get; set; }


    }
}
