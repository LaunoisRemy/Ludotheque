using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    public class AssignedTheme
    {
        public int ThemeId { get; set; }
        public string Theme { get; set; }
        public bool Assigned { get; set; }
    }
}
