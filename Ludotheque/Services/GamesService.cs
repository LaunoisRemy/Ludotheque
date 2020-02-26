using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ludotheque.Data;
using Ludotheque.Models;
using Microsoft.EntityFrameworkCore;

namespace Ludotheque.Services
{
    public class GamesService
    {
        private readonly LudothequeContext _context;

        /// <summary>
        /// Constructor of GamesService to work on the model Game
        /// </summary>
        /// <param name="context">Database of the application where we can find game</param>
        public GamesService(LudothequeContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get all games
        /// </summary>
        /// <returns>A list of all games in Database </returns>
        public IQueryable<Game> GetGames()
        {
            return from g in _context.Games
                select g;
        }
        /// <summary>
        /// Get all games that contain the searchString in name
        /// </summary>
        /// <param name="searchString">Param wanted by user</param>
        /// <param name="gamesList">If we want do this research on a precise lsit of games</param>
        /// <returns></returns>
        public IQueryable<Game> GetGamesByName(string searchString,IQueryable<Game> gamesList = null)
        {
            if (gamesList == null)
            {
                gamesList = GetGames();
            }
            return gamesList.Where(s => s.Name.Contains(searchString));
        }
        public async Task<Game> GetGameById(int id)
        {
            Game game = await _context.Games.FirstOrDefaultAsync(m => m.Id == id);

            return game;
        }

        public async Task AddGame(Game game)
        {
            _context.Add(game);
            await _context.SaveChangesAsync();
        }

    }
}
