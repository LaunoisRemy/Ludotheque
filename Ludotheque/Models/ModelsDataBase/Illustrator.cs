using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    public class Illustrator
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public IList<Game> Games { get; set; }

    }
}
