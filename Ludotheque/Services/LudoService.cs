using Ludotheque.Data;
using Ludotheque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Services
{
    public class LudoService
    {
        private readonly LudothequeAccountContext _context;


        /// <summary>
        /// Constructor of GamesService to work on the model Game
        /// </summary>
        /// <param name="context">Database of the application where we can find game</param>
        public LudoService(LudothequeAccountContext context)
        {
            _context = context;
        }



    }
}
