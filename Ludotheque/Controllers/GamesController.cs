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
using Microsoft.AspNetCore.Authorization;

namespace Ludotheque.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly LudothequeAccountContext _context;
        private GamesService _gameService;
        private GameAllDataService _gameAllDataService;

        public GamesController(LudothequeAccountContext context)
        {
            _context = context;
            _gameService = new GamesService(context);;
            _gameAllDataService = new GameAllDataService(context);;

        }

        // GET: Games
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString,string sortOrder, string currentFilter, int? pageNumber)
        {
            //Todo : Si ecran trop petit afficher des colonnes en moins
            //Todo : limiter le nombre de catégories montrées a 3
            //Todo : Probleme si critères vide sort ne trie pas bien
            IQueryable<Game> games;
            if (!String.IsNullOrEmpty(searchString))
            {
                games = _gameService.GetGamesByName(searchString);
                pageNumber = 1;
            }
            else
            {
                games = _gameService.GetGames();
                searchString = currentFilter;
            }
            games = _gameService.GetGamesValidate(games);
            GamesIndexData gamesAllData = await SortGames(searchString,sortOrder,currentFilter,pageNumber,games);


            return View(gamesAllData);
        }
        /// <summary>
        /// Controller to list games of one editor 
        /// </summary>
        /// <param name="id">id of game</param>
        /// <param name="sortOrder"> string for know how to sort columns</param>
        /// <param name="currentFilter">Filtrer apply</param>
        /// <param name="pageNumber">Number of page</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Editor(int id, string sortOrder, string currentFilter, int? pageNumber)
        {

            var editor = await _context.Editors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (editor == null)
            {
                return NotFound();
            }

            ViewBag.EditorName = editor.Name;
            ViewBag.EditorDesc = editor.Description;

            IQueryable<Game> games = _gameService.GetGamesByEditor(id);
            GamesIndexData gamesAllData = await SortGames(currentFilter, sortOrder, currentFilter, pageNumber, games);
            return View(gamesAllData);
        }
        /// <summary>
        /// Controller to show all games of one editor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sortOrder"></param>
        /// <param name="currentFilter"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Theme(int id, string sortOrder, string currentFilter, int? pageNumber)
        {

            var t = await _context.Theme
                .FirstOrDefaultAsync(m => m.Id == id);
            if (t == null)
            {
                return NotFound();
            }

            ViewBag.ThemeName = t.Name;
            ViewBag.ThemeDesc = t.Description;

            IQueryable<Game> games = _gameService.GetGamesByTheme(id);
            GamesIndexData gamesAllData = await SortGames(currentFilter, sortOrder, currentFilter, pageNumber, games);
            return View(gamesAllData);
        }
        /// <summary>
        /// Controller to show all games of one type of support
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sortOrder"></param>
        /// <param name="currentFilter"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> MaterialSupport(int id, string sortOrder, string currentFilter, int? pageNumber)
        {
            var t = await _context.MaterialSupport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (t == null)
            {
                return NotFound();
            }

            ViewBag.MsName = t.Name;
            ViewBag.MsDesc = t.Description;

            IQueryable<Game> games = _gameService.GetGamesByMs(id);
            GamesIndexData gamesAllData = await SortGames(currentFilter, sortOrder, currentFilter, pageNumber, games);
            return View(gamesAllData);
        }
        /// <summary>
        /// Controller to show all games of one type of mechanism
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sortOrder"></param>
        /// <param name="currentFilter"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Mechanism(int id, string sortOrder, string currentFilter, int? pageNumber)
        {

            var t = await _context.Mechanism
                .FirstOrDefaultAsync(m => m.Id == id);
            if (t == null)
            {
                return NotFound();
            }

            ViewBag.MName = t.Name;
            ViewBag.MDesc = t.Description;

            IQueryable<Game> games = _gameService.GetGamesByMecha(id);
            GamesIndexData gamesAllData = await SortGames(currentFilter, sortOrder, currentFilter, pageNumber, games);
            return View(gamesAllData);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Difficulty(int id, string sortOrder, string currentFilter, int? pageNumber)
        {
            var t = await _context.Difficulties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (t == null)
            {
                return NotFound();
            }

            ViewBag.DiffName = t.label;

            IQueryable<Game> games = _gameService.GetGamesByDifficulty(id);
            GamesIndexData gamesAllData = await SortGames(currentFilter, sortOrder, currentFilter, pageNumber, games);
            return View(gamesAllData);
        }

        [AllowAnonymous]
        public async Task<GamesIndexData> SortGames(string searchString, string sortOrder, string currentFilter, int? pageNumber, IQueryable<Game> games)
        {
            ViewBag.CurrentSort = sortOrder;

            // Sort by column
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.MinPlSortParam = sortOrder == "Min" ? "min_desc" : "Min";
            ViewBag.MaxPlSortParam = sortOrder == "Max" ? "max_desc" : "Max";
            ViewBag.AgeSortParam = sortOrder == "Age" ? "age_desc" : "Age";
            ViewBag.TimeSortParam = sortOrder == "Time" ? "time_desc" : "Time";
            ViewBag.DiffSortParam = sortOrder == "DIff" ? "diff_desc" : "Diff";
            ViewBag.IlluSortParam = sortOrder == "Illu" ? "Illu_desc" : "Illu";
            ViewBag.EditorSortParam = sortOrder == "Editor" ? "editor_desc" : "Editor";
            ViewBag.CurrentFilter = searchString;

            GamesIndexData gamesAllData = new GamesIndexData();



            games = _gameService.SortGames(games, sortOrder);
            //gamesAllData = _gameAllDataService.SortGamesIndexData(gamesAllData, sortOrder);

            //games =  gamesAllData.Games.AsQueryable();
            int pageSize = 3;
            PaginatedList<Game> pl =
                await PaginatedList<Game>.CreateAsync(games, pageNumber ?? 1, pageSize);
            var gamesTmp = from g in games
                where pl.Contains(g)
                select g;

            gamesAllData = await _gameAllDataService.GetGamesAndCategories(gamesTmp);
            gamesAllData.PageIndex = pl.PageIndex;
            gamesAllData.TotalPages = pl.TotalPages;
            return gamesAllData;

        }

        // GET: Games/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            //todo: details n'est pas la vue d'un jeu mais de son etat

            if (id == null)
            {
                return NotFound();
            }
            else
            {
                int _id = (int) id;
                var game = await _gameService.GetGameWithCategories((int)id);

                if (game == null)
                {
                    return NotFound();
                }

                ViewDataRelationMtM(game);
                ViewDataRelationOtM(game);
                return View(game);
            }


        }

        // GET: Games/Create
        [Authorize]
        public IActionResult Create()
        {
            var game = new Game();
            game.ThemesGames = new List<ThemesGames>();
            game.MaterialSupportsGames = new List<MaterialSupportsGames>();
            game.MechanismsGames = new List<MechanismsGames>();
            ViewDataRelationMtM(game);
            ViewDataRelationOtM(game);

            return View();
        }


        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,MinPlayer,MaxPlayer,MinimumAge,GameTime,Price,ReleaseDate,BuyLink,VideoLink,PictureLink,Validate,DifficultyId,IllustratorId,EditorId")] Game game, string[] selectedThemes, string[] selectedMs, string[] selectedMecha)
        {

            if (selectedMecha != null)
            {
                game.MechanismsGames = new List<MechanismsGames>();
                foreach (var mecha in selectedMecha)
                {
                    var mechaToAdd = new MechanismsGames() { GameId = game.Id, MechanismId = int.Parse(mecha) };
                    game.MechanismsGames.Add(mechaToAdd);
                }
            }
            if (selectedMs != null)
            {
                game.MaterialSupportsGames = new List<MaterialSupportsGames>();
                foreach (var ms in selectedMs)
                {
                    var msToAdd = new MaterialSupportsGames() { GameId = game.Id, MaterialSupportId = int.Parse(ms) };
                    game.MaterialSupportsGames.Add(msToAdd);
                }
            }
            if (selectedThemes != null)
            {
                game.ThemesGames = new List<ThemesGames>();
                foreach (var t in selectedThemes)
                {
                    var themeToAdd = new ThemesGames() { GameId = game.Id, ThemeId = int.Parse(t) };
                    game.ThemesGames.Add(themeToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewDataRelationOtM(game);

            ViewDataRelationMtM(game);
            return View(game);
        }

        // GET: Games/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var game = await _gameService.GetGameWithCategories((int) id);
            //var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            ViewDataRelationMtM(game);
            ViewDataRelationOtM(game);
            //var t =  await _gameAllDataService.GetGameAndCategories(game);
            return View(game);
        }


        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,MinPlayer,MaxPlayer,MinimumAge,GameTime,Price,ReleaseDate,BuyLink,VideoLink,PictureLink,Validate,DifficultyId,IllustratorId,EditorId")] Game game, string[] selectedThemes, string[] selectedMs, string[] selectedMecha)
        {
            decimal prix = game.Price;
            if (id != game.Id)
            {
                return NotFound();
            }

            var gameToUpdate = await _gameService.GetGameWithCategories(id);

            if (await TryUpdateModelAsync<Game>(
                gameToUpdate,
                "",
                i => i.Name, i => i.Description, i => i.MinPlayer, i => i.MaxPlayer
                , i => i.MinimumAge, i => i.GameTime, i => i.Price
                , i => i.ReleaseDate, i => i.BuyLink, i => i.VideoLink,
                i => i.PictureLink, i => i.Validate, i => i.DifficultyId, i => i.IllustratorId, i => i.EditorId))
            {
                _gameService.UpdateGamesThemes(selectedThemes, gameToUpdate);
                _gameService.UpdateGamesMaterialSupport(selectedMs, gameToUpdate);
                _gameService.UpdateGamesMechanisms(selectedMecha, gameToUpdate);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                 "Try again, and if the problem persists, " +
                                                 "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            _gameService.UpdateGamesThemes(selectedThemes, gameToUpdate);
            _gameService.UpdateGamesMaterialSupport(selectedMs, gameToUpdate);
            _gameService.UpdateGamesMechanisms(selectedMecha, gameToUpdate);

            ViewDataRelationMtM(gameToUpdate);
            return View(gameToUpdate);
        }


        // GET: Games/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int _id = (int)id;
            var game = await _gameService.GetGameById(_id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //===================== Methods for controller
        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }

        private void ViewDataRelationMtM(Game game)
        {
            var viewModel = _gameService.PopulateAssignedThemesData(game);
            ViewData["Theme"] = viewModel;
            viewModel = _gameService.PopulateAssignedMsData(game);
            ViewData["MaterialSupport"] = viewModel;
            viewModel = _gameService.PopulateAssignedMechasData(game);
            ViewData["Mechanisms"] = viewModel;

        }

        private void ViewDataRelationOtM(Game game)
        {
            ViewData["DifficultyId"] = new SelectList(_context.Difficulties, "Id", "label", game.DifficultyId);
            ViewData["EditorId"] = new SelectList(_context.Editors, "Id", "Name", game.EditorId);
            ViewData["IllustratorId"] = new SelectList(_context.Illustrators, "Id", "LastName", game.IllustratorId);
        }
    }
}
