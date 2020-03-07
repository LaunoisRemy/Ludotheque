using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ludotheque.Data;
using Ludotheque.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Ludotheque.Services
{
    /// <summary>
    /// Class for methods on the object Game
    /// </summary>
    public class GamesService
    {
        private readonly LudothequeAccountContext _context;

        /// <summary>
        /// Constructor of GamesService to work on the model Game
        /// </summary>
        /// <param name="context">Database of the application where we can find game</param>
        public GamesService(LudothequeAccountContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get all games
        /// </summary>
        /// <returns>A list of all games in Database </returns>
        public IQueryable<Game> GetGames()
        {
            return _context.Games.Include(g => g.Difficulty)
                .Include(g => g.Editor)
                .Include(g => g.Illustrator);


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
        /// <summary>
        /// Get all games validate by admin
        /// </summary>
        /// <param name="gamesList">The list to filter</param>
        /// <returns></returns>
        public IQueryable<Game> GetGamesValidate(IQueryable<Game> gamesList )
        {
            return  from g in gamesList
                        where g.Validate == true
                        select g;
        }
        /// <summary>
        /// Get all games no validate by admin
        /// </summary>
        /// <param name="gamesList">The list to filter</param>
        /// <returns></returns>
        public IQueryable<Game> GetGamesNoValidate(IQueryable<Game> gamesList)
        {
            return from g in gamesList
                   where g.Validate == false
                   select g;
        }
        /// <summary>
        /// Get game by id
        /// </summary>
        /// <param name="id"> id of the game looking for</param>
        /// <returns>Task game for asynchrone</returns>
        public async Task<Game> GetGameById(int id)
        {
            Game game = await _context.Games
                .Include(g => g.Difficulty)
                .Include(g => g.Editor)
                .Include(g => g.Illustrator)
                .FirstOrDefaultAsync(m => m.Id == id);

            return game;
        }



        /// <summary>
        /// Get game by editor
        /// </summary>
        /// <param name="id"> id editor of the games we looking for</param>
        /// <returns> game for asynchrone</returns>
        public IQueryable<Game> GetGamesByEditor(int id)
        {
            return _context.Games.Include(g => g.Difficulty)
                .Include(g => g.Editor)
                .Include(g => g.Illustrator)
                .Where(c =>c.EditorId == id);

        }
        /// <summary>
        /// Get game by theme
        /// </summary>
        /// <param name="id"> id theme of the games we looking for</param>
        /// <returns> game for asynchrone</returns>
        public IQueryable<Game> GetGamesByTheme(int id)
        {
            var idGames= from gt in _context.ThemesGames
                where gt.ThemeId == id
                select gt.Game.Id;

            return _context.Games.Include(g => g.Difficulty)
                .Include(g => g.Editor)
                .Include(g => g.Illustrator)
                .Where(c => idGames.Contains(c.Id));

        }
        /// <summary>
        /// Get game by material support
        /// </summary>
        /// <param name="id"> id material support of the games we looking for</param>
        /// <returns> game for asynchrone</returns>
        public IQueryable<Game> GetGamesByMs(int id)
        {
            var idGames = from gt in _context.MaterialSupportsGames
                where gt.MaterialSupportId == id
                select gt.Game.Id;

            return _context.Games.Include(g => g.Difficulty)
                .Include(g => g.Editor)
                .Include(g => g.Illustrator)
                .Where(c => idGames.Contains(c.Id));

        }
        /// <summary>
        /// Get game by mechanisms support
        /// </summary>
        /// <param name="id"> id mechanisms of the games we looking for</param>
        /// <returns> game for asynchrone</returns>
        public IQueryable<Game> GetGamesByMecha(int id)
        {
            var idGames = from gt in _context.MechanismsGames
                where gt.MechanismId == id
                select gt.Game.Id;

            return _context.Games.Include(g => g.Difficulty)
                .Include(g => g.Editor)
                .Include(g => g.Illustrator)
                .Where(c => idGames.Contains(c.Id));

        }
        /// <summary>
        /// Get game by difficulty support
        /// </summary>
        /// <param name="id"> id difficulty of the games we looking for</param>
        /// <returns> game for asynchrone</returns>
        public IQueryable<Game> GetGamesByDifficulty(int id)
        {
            return _context.Games.Include(g => g.Difficulty)
                .Include(g => g.Difficulty)
                .Include(g => g.Illustrator)
                .Where(c => c.DifficultyId == id);
        }
        /// <summary>
        /// Get game by user
        /// </summary>
        /// <param name="id">  user id of the games we looking for</param>
        /// <returns> game </returns>
        public IQueryable<Game> GetGamesByUser(string id)
        {
            var idGames = from gt in _context.GamesUser
                          where gt.LudothequeUserId.Equals(id)
                          select gt.Game.Id;

            return _context.Games.Include(g => g.Difficulty)
                .Include(g => g.Editor)
                .Include(g => g.Illustrator)
                .Where(c => idGames.Contains(c.Id));
        }
        /// <summary>
        /// Add game in database
        /// </summary>
        /// <param name="game">Game to add</param>
        /// <returns></returns>
        public async Task AddGame(Game game)
        {
            _context.Add(game);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Sort list of game by order
        /// </summary>
        /// <param name="games">List of games not sorted</param>
        /// <param name="sortOrder">How sort list of games</param>
        /// <returns></returns>
        public IQueryable<Game> SortGames(IQueryable<Game> games, string sortOrder)
        {
            games = sortOrder switch
            {
                "name_desc" => games.OrderByDescending(s => s.Name),
                "Date" => games.OrderBy(s => s.ReleaseDate),
                "date_desc" => games.OrderByDescending(s => s.ReleaseDate),
                "Price" => games.OrderBy(s => s.Price),
                "price_desc" => games.OrderByDescending(s => s.Price),
                "Min" => games.OrderBy(s => s.MinPlayer),
                "min_desc" => games.OrderByDescending(s => s.MinPlayer),
                "Max" => games.OrderBy(s => s.MaxPlayer),
                "max_desc" => games.OrderByDescending(s => s.MaxPlayer),
                "Age" => games.OrderBy(s => s.MinimumAge),
                "age_desc" => games.OrderByDescending(s => s.MinimumAge),
                "Time" => games.OrderBy(s => s.GameTime),
                "time_desc" => games.OrderByDescending(s => s.GameTime),
                _ => games.OrderBy(s => s.Name),
            };
            return games;
        }
        internal GamesIndexData SortGames(GamesIndexData gamesAllData, string sortOrder)
        {
            var games = gamesAllData.Games;
            games = sortOrder switch
            {
                "name_desc" => games.OrderByDescending(s => s.Name),
                "Date" => games.OrderBy(s => s.ReleaseDate),
                "date_desc" => games.OrderByDescending(s => s.ReleaseDate),
                "Price" => games.OrderBy(s => s.Price),
                "price_desc" => games.OrderByDescending(s => s.Price),
                "Min" => games.OrderBy(s => s.MinPlayer),
                "min_desc" => games.OrderByDescending(s => s.MinPlayer),
                "Max" => games.OrderBy(s => s.MaxPlayer),
                "max_desc" => games.OrderByDescending(s => s.MaxPlayer),
                "Age" => games.OrderBy(s => s.MinimumAge),
                "age_desc" => games.OrderByDescending(s => s.MinimumAge),
                "Time" => games.OrderBy(s => s.GameTime),
                "time_desc" => games.OrderByDescending(s => s.GameTime),
                _ => games.OrderBy(s => s.Name),
            };
            gamesAllData.Games = games;
            return gamesAllData;
        }

        /// <summary>
        /// Method get all categories ( Themes,Material support, Mechanisms) of a game
        /// </summary>
        /// <param name="id">id of the game</param>
        /// <returns>game with all categories</returns>
        public async Task<Game> GetGameWithCategories(int id)
        {
            Game g = await _context.Games
                .Include(i => i.ThemesGames)
                .ThenInclude(i => i.Theme)
                .Include(i => i.MaterialSupportsGames)
                .ThenInclude(i => i.MaterialSupport)
                .Include(i => i.MechanismsGames)
                .ThenInclude(i => i.Mechanism)
                .FirstOrDefaultAsync(m => m.Id == id);
            return g;
        }



            //===================== Methos who concern relation many to many, very similar ==========================
            //Todo : factorize code but how?

            /// <summary>
            /// Give a list of Theme for a game
            /// </summary>
            /// <param name="game">Game we are looking theme</param>
            /// <returns>List of theme of the game</returns>
            public List<AssignedCategories> PopulateAssignedThemesData(Game game)
        {
            var gameTheme = new HashSet<int>(game.ThemesGames.Select(c => c.ThemeId));
            var allTheme = _context.Theme;
            var viewModel = new List<AssignedCategories>();
            foreach (var theme in allTheme)
            {
                viewModel.Add(new AssignedCategories()
                {
                    CategoryId = theme.Id,
                    Name = theme.Name,
                    Assigned = gameTheme.Contains(theme.Id)
                });
            }
            return viewModel;
        }
        /// <summary>
        /// Give a list of Material support for a game
        /// </summary>
        /// <param name="game">Game we are looking Material support</param>
        /// <returns>List of Material support of the game</returns>
        public List<AssignedCategories> PopulateAssignedMsData(Game game)
        {
            var gameMs = new HashSet<int>(game.MaterialSupportsGames.Select(c =>c.MaterialSupportId));
            var allMs = _context.MaterialSupport;
            var viewModel = new List<AssignedCategories>();
            foreach (var ms in allMs)
            {
                viewModel.Add(new AssignedCategories()
                {
                    CategoryId = ms.Id,
                    Name = ms.Name,
                    Assigned = gameMs.Contains(ms.Id)
                });
            }


            return viewModel;
        }
        /// <summary>
        /// Give a list of Mechanisms for a game
        /// </summary>
        /// <param name="game">Game we are looking Mechanisms </param>
        /// <returns>List of Mechanisms of the game</returns>
        public List<AssignedCategories> PopulateAssignedMechasData(Game game)
        {
            HashSet<int> gameM;
            gameM = new HashSet<int>(game.MechanismsGames.Select(c => c.MechanismId));
            var allM = _context.Mechanism;

            var viewModel = new List<AssignedCategories>();
            foreach (var mecha in allM)
            {
                viewModel.Add(new AssignedCategories()
                {
                    CategoryId = mecha.Id,
                    Name = mecha.Name,
                    Assigned = gameM.Contains(mecha.Id)
                });
            }
            return viewModel;
        }
        /// <summary>
        /// Method to update themes of a game
        /// </summary>
        /// <param name="selectedThemes">Theme to apply</param>
        /// <param name="gameToUpdate">Game that needs changes</param>
        public void UpdateGamesThemes(string[] selectedThemes, Game gameToUpdate)
        {
            if (selectedThemes == null)
            {
                gameToUpdate.ThemesGames = new List<ThemesGames>();
                return;
            }

            var selectedThems = new HashSet<string>(selectedThemes);
            var gamesThemes = new HashSet<int>
                (gameToUpdate.ThemesGames.Select(c => c.Theme.Id));
            foreach (var theme in _context.Theme)
            {
                if (selectedThems.Contains(theme.Id.ToString()))
                {
                    if (!gamesThemes.Contains(theme.Id))
                    {
                        gameToUpdate.ThemesGames.Add(new ThemesGames { GameId = gameToUpdate.Id, ThemeId = theme.Id });
                    }
                }
                else
                {

                    if (gamesThemes.Contains(theme.Id))
                    {
                        ThemesGames themsToRemove = gameToUpdate.ThemesGames.FirstOrDefault(i => i.ThemeId == theme.Id);
                        _context.Remove(themsToRemove);
                    }
                }
            }
        }
        /// <summary>
        /// Method to update materials supports of a game
        /// </summary>
        /// <param name="selectedMaterials">materials to apply</param>
        /// <param name="gameToUpdate">Game that needs changes</param>
        public void UpdateGamesMaterialSupport(string[] selectedMaterials, Game gameToUpdate)
        {
            if (selectedMaterials == null)
            {
                gameToUpdate.MaterialSupportsGames = new List<MaterialSupportsGames>();
                return;
            }

            var selectedMs = new HashSet<string>(selectedMaterials);
            var gamesMs = new HashSet<int>
                (gameToUpdate.MaterialSupportsGames.Select(c => c.MaterialSupport.Id));
            foreach (var ms in _context.MaterialSupport)
            {
                if (selectedMs.Contains(ms.Id.ToString()))
                {
                    if (!gamesMs.Contains(ms.Id))
                    {
                        gameToUpdate.MaterialSupportsGames.Add(new MaterialSupportsGames() { GameId = gameToUpdate.Id, MaterialSupportId = ms.Id });
                    }
                }
                else
                {
                    if (gamesMs.Contains(ms.Id))
                    {
                        MaterialSupportsGames msToRemove = gameToUpdate.MaterialSupportsGames.FirstOrDefault(i => i.MaterialSupportId == ms.Id);
                        _context.Remove(msToRemove);
                    }
                }
            }
        }
        /// <summary>
        /// Method to update mechanisms of a game
        /// </summary>
        /// <param name="selectedMechanisms">mechanisms to apply</param>
        /// <param name="gameToUpdate">Game that needs changes</param>
        public void UpdateGamesMechanisms(string[] selectedMechanisms, Game gameToUpdate)
        {
            if (selectedMechanisms == null)
            {
                gameToUpdate.MechanismsGames = new List<MechanismsGames>();
                return;
            }

            var selectedMecha = new HashSet<string>(selectedMechanisms);
            var gamesMecha = new HashSet<int>
                (gameToUpdate.MechanismsGames.Select(c => c.Mechanism.Id));
            foreach (var m in _context.Mechanism)
            {
                if (selectedMecha.Contains(m.Id.ToString()))
                {
                    if (!gamesMecha.Contains(m.Id))
                    {
                        gameToUpdate.MechanismsGames.Add(new MechanismsGames() { GameId = gameToUpdate.Id, MechanismId = m.Id });
                    }
                }
                else
                {
                    if (gamesMecha.Contains(m.Id))
                    {
                        MechanismsGames mToRemove = gameToUpdate.MechanismsGames.FirstOrDefault(i => i.MechanismId == m.Id);
                        _context.Remove(mToRemove);
                    }
                }
            }
        }

    }
}
