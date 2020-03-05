using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ludotheque.Models;
using Microsoft.AspNetCore.Identity;

namespace Ludotheque.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the LudothequeUser class
    public class LudothequeUser : IdentityUser
    {
        public ICollection<GamesUser> GamesUser { get; set; }

    }
}
