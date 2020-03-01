using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    /// <summary>
    /// Class to present a game on the index page ( with all categories)
    /// </summary>
    public class GamesIndexData
    {
        //todo : Revenir sur se systeme de pagination peu fiable 
        public int PageIndex { get;  set; }
        public int TotalPages { get; set; }

        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<Theme> Themes { get; set; }
        public IEnumerable<MaterialSupport> MaterialSupports { get; set; }
        public IEnumerable<Mechanism> Mechanisms { get; set; }
        /// <summary>
        /// used to enable or disable Previous
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        /// <summary>
        ///  used to enable or disable Next paging buttons
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }
}
