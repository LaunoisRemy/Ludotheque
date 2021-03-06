﻿using Ludotheque.Areas.Identity.Data;
using Ludotheque.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ludotheque.Services
{
    /// <summary>
    /// Class to deal with users
    /// </summary>
    public class UserServices
    {
        /// <summary>
        /// Method to get user by name
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<LudothequeUser> GetUserAsync(UserManager<LudothequeUser> userManager, string name)
        {
            var u = await userManager.FindByNameAsync(name);
            var userTmp = await userManager.FindByIdAsync(u.Id);
            return userTmp;
        }
    }
}
