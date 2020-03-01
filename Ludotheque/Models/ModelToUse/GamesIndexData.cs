using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    public class GamesIndexData
    {
        //todo : Revenir sur se systeme de pagination peu fiable 
        public int PageIndex { get;  set; }
        public int TotalPages { get; set; }

        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<Theme> Themes { get; set; }
        public IEnumerable<MaterialSupport> MaterialSupports { get; set; }
        public IEnumerable<Mechanism> Mechanisms { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }
}
