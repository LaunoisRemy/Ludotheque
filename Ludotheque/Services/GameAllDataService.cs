using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ludotheque.Data;
using Ludotheque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Ludotheque.Services
{
    public class GameAllDataService
    {
        private readonly LudothequeContext _context;
        private GamesService _gameServices;


        /// <summary>
        /// Constructor of GamesAllDataService to work on the model Game
        /// </summary>
        /// <param name="context">Database of the application where we can find game</param>
        public GameAllDataService(LudothequeContext context)
        {
            _context = context;
            _gameServices = new GamesService(_context);

        }

        /// <summary>
        /// Method to create games with theme, mechanisms and materials supports
        /// </summary>
        /// <returns></returns>
        public async Task<GamesIndexData> GetGamesAndCategories(IQueryable<Game> games=null)
        {
            if (games == null)
            {
                games = _context.Games;
            }
            var viewModel = new GamesIndexData();
            viewModel.Games = await games
                .Include(i => i.MechanismsGames)
                .ThenInclude(i => i.Mechanism)
                .Include(i => i.ThemesGames)
                .ThenInclude(i => i.Theme)
                .Include(i => i.MaterialSupportsGames)
                .ThenInclude(i => i.MaterialSupport)
                .AsNoTracking()
                .OrderBy(i => i.Name)
                .ToListAsync();
            return viewModel;

        }
        public async Task<GameAllData> GetGameAndCategories(Game game)
        {
            var theme = from tg in game.ThemesGames 
                join t in _context.Theme on tg.Theme equals t
                select t;
            var matSup = from ms in game.MaterialSupportsGames
                join mat in _context.MaterialSupport on ms.MaterialSupport equals mat
                select mat;
            var mecha = from m in game.MechanismsGames
                join me in _context.Mechanism on m.Mechanism equals me
                select me;
            var res = new GameAllData();
            res.Game = game;
            res.Themes = theme;
            res.MaterialSupports = matSup;
            res.Mechanisms = mecha;
            return res;

        }

        /// <summary>
        /// Get all games that contain the searchString in name
        /// </summary>
        /// <param name="searchString">Param wanted by user</param>
        /// <param name="gamesList">If we want do this research on a precise lsit of games</param>
        /// <returns></returns>
        public async Task<GamesIndexData> GetGamesByName(string searchString, GamesIndexData gamesList = null)
        {
            var games = _gameServices.GetGamesByName(searchString); ;
            return await GetGamesAndCategories(games);
        }


        public GamesIndexData SortGamesIndexData(GamesIndexData games, string sortOrder)
        {

            games.Games = _gameServices.SortGames(games.Games.AsQueryable(), sortOrder);
            return games;
        }


        public List<Game> GetGamesPages(GamesIndexData games, int pageNumber, int pageSize)
        {
            var PageIndex = pageNumber;
            var count = games.Games.Count();
            var items = games.Games.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            return items;
        }



    }
}
