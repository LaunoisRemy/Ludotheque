using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ludotheque.Data;
using Ludotheque.Models;
using Ludotheque.Services;

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
            return _context.Games.Include(g => g.Difficulty).Include(g => g.Editor).Include(g => g.Illustrator);


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
            Game game = await _context.Games
                .Include(g => g.Difficulty)
                .Include(g => g.Editor)
                .Include(g => g.Illustrator)
                .FirstOrDefaultAsync(m => m.Id == id);

            return game;
        }

        public async Task AddGame(Game game)
        {
            _context.Add(game);
            await _context.SaveChangesAsync();
        }


        public IQueryable<Game> SortGames(IQueryable<Game> games, string sortOrder)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    games = games.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    games = games.OrderBy(s => s.ReleaseDate);
                    break;
                case "date_desc":
                    games = games.OrderByDescending(s => s.ReleaseDate);
                    break;
                case "Price":
                    games = games.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    games = games.OrderByDescending(s => s.Price);
                    break;
                case "Min":
                    games = games.OrderBy(s => s.MinPlayer);
                    break;
                case "min_desc":
                    games = games.OrderByDescending(s => s.MinPlayer);
                    break;
                case "Max":
                    games = games.OrderBy(s => s.MaxPlayer);
                    break;
                case "max_desc":
                    games = games.OrderByDescending(s => s.MaxPlayer);
                    break;
                case "Age":
                    games = games.OrderBy(s => s.MinimumAge);
                    break;
                case "age_desc":
                    games = games.OrderByDescending(s => s.MinimumAge);
                    break;
                case "Time":
                    games = games.OrderBy(s => s.GameTime);
                    break;
                case "time_desc":
                    games = games.OrderByDescending(s => s.GameTime);
                    break;
                default:
                    games = games.OrderBy(s => s.Name);
                    break;
            }

            return games;
        }

        public IQueryable<Game> GetGamesPages(IQueryable<Game> games, int pageNumber, int pageSize)
        {
            var PageIndex = pageNumber;
            var count = games.Count();
            var items = games.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            var hasPrevious = (PageIndex > 1);
            var hasNext = (PageIndex < TotalPages);

            return items.AsQueryable();
        }




    }
}
