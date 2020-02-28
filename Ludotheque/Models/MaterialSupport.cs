using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    public class MaterialSupport
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nom")]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<MaterialSupportsGames> MaterialSupportsGames { get; set; }

    }
}
