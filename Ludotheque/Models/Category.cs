using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    public enum Type
    {
        Theme, Mecanism, MaterialSupport
    }
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public Type Type { get; set; }

    }
}
