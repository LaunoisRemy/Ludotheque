using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ludotheque.Data;
using Ludotheque.Models;
using Ludotheque.Models;
using Ludotheque.Services;

namespace Ludotheque.Controllers
{
    public class GamesController : Controller
    {
        private readonly LudothequeContext _context;
        private GamesService _gameService;
        private GameAllDataService _gameAllDataService;

        public GamesController(LudothequeContext context)
        {
            _context = context;
            _gameService = new GamesService(context);;
            _gameAllDataService = new GameAllDataService(context);;

        }

        // GET: Games
        public async Task<IActionResult> Index(string searchString,string sortOrder, string currentFilter, int? pageNumber)
        {
            //Todo : Si ecran trop petit afficher des colonnes en moins
            //Todo : Probleme si critères vide sort ne trie pas bien
            //return View(await _context.Jeu.ToListAsync());
            IQueryable<Game> games;

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

            if (!String.IsNullOrEmpty(searchString))
            {
                games = _gameService.GetGamesByName(searchString);
                //gamesAllData = await  _gameAllDataService.GetGamesByName(searchString);
                pageNumber = 1;
            }
            else
            {
                games = _gameService.GetGames();
                //gamesAllData = await _gameAllDataService.GetGamesAndCategories();
                searchString = currentFilter;
            }

            games = _gameService.SortGames(games, sortOrder);
            //gamesAllData = _gameAllDataService.SortGamesIndexData(gamesAllData, sortOrder);

            //games =  gamesAllData.Games.AsQueryable();
            int pageSize = 3;
            PaginatedList<Game> pl =
                await PaginatedList<Game>.CreateAsync(games, pageNumber ?? 1, pageSize);
            var gTest = from g in games
                where pl.Contains(g)
                select g;
            
            gamesAllData = await _gameAllDataService.GetGamesAndCategories(gTest);
            gamesAllData.PageIndex = pl.PageIndex;
            gamesAllData.TotalPages = pl.TotalPages;

            return View(gamesAllData);
            //return View(await games.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                int _id = (int) id;
                var game = await _gameService.GetGameById(_id);
                if (game == null)
                {
                    return NotFound();
                }

                return View(game);
            }


        }

        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["DifficultyId"] = new SelectList(_context.Difficulties, "Id", "label");
            ViewData["EditorId"] = new SelectList(_context.Editors, "Id", "Name");
            ViewData["IllustratorId"] = new SelectList(_context.Illustrators, "Id", "LastName");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,MinPlayer,MaxPlayer,MinimumAge,GameTime,Price,ReleaseDate,BuyLink,VideoLink,PictureLink,Validate,DifficultyId,IllustratorId,EditorId")] Game game)
        {
            if (ModelState.IsValid)
            {
                await _gameService.AddGame(game);
                return RedirectToAction(nameof(Index));
            }
            ViewData["DifficultyId"] = new SelectList(_context.Difficulties, "Id", "Id", game.DifficultyId);
            ViewData["EditorId"] = new SelectList(_context.Editors, "Id", "Name", game.EditorId);
            ViewData["IllustratorId"] = new SelectList(_context.Illustrators, "Id", "LastName", game.IllustratorId);
            return View(game);
        }

        // GET: Games/Edit/5
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
            var viewModel= _gameService.PopulateAssignedThemesData(game);
            ViewData["Theme"] = viewModel;
            viewModel = _gameService.PopulateAssignedMsData(game);
            ViewData["MaterialSupport"] = viewModel;
            viewModel = _gameService.PopulateAssignedMechasData(game);
            ViewData["Mechanisms"] = viewModel;

            ViewData["DifficultyId"] = new SelectList(_context.Difficulties, "Id", "label", game.DifficultyId);
            ViewData["EditorId"] = new SelectList(_context.Editors, "Id", "Name", game.EditorId);
            ViewData["IllustratorId"] = new SelectList(_context.Illustrators, "Id", "LastName", game.IllustratorId);
            //var t =  await _gameAllDataService.GetGameAndCategories(game);
            return View(game);
        }


        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

            var viewModel = _gameService.PopulateAssignedThemesData(gameToUpdate);
            ViewData["Theme"] = viewModel;
            viewModel = _gameService.PopulateAssignedMechasData(gameToUpdate);
            ViewData["Mechanisms"] = viewModel;
            viewModel = _gameService.PopulateAssignedMsData(gameToUpdate);
            ViewData["MaterialSupport"] = viewModel;
            viewModel = _gameService.PopulateAssignedMechasData(gameToUpdate);
            ViewData["Mechanisms"] = viewModel;
            return View(gameToUpdate);
        }


        // GET: Games/Delete/5
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }

    }
}
