using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    public class AssignedCategories
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}
