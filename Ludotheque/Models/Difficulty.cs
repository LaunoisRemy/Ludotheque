using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    public enum Label
    {
        [Description("Facile")]
        Easy,
        [Description("Facile")]
        Moderate,
        [Description("Difficile")]
        Hard
    }

    public class Difficulty
    {
        public int Id { get; set; }
        [Required]
        public Label label { get; set; }
    }


}
