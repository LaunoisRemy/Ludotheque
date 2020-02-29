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

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["DifficultyId"] = new SelectList(_context.Difficulties, "Id", "label", game.DifficultyId);
            ViewData["EditorId"] = new SelectList(_context.Editors, "Id", "Name", game.EditorId);
            ViewData["IllustratorId"] = new SelectList(_context.Illustrators, "Id", "LastName", game.IllustratorId);
            var t =  await _gameAllDataService.GetGameAndCategories(game);
            return View(t);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,MinPlayer,MaxPlayer,MinimumAge,GameTime,Price,ReleaseDate,BuyLink,VideoLink,PictureLink,Validate,DifficultyId,IllustratorId,EditorId")] Game game)
        {
            decimal prix = game.Price;
            if (id != game.Id)
            {
                return NotFound();
            }
            //Todo : Verifier en jquery mais de manière européenne le prix, pour l'instant désactivé
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DifficultyId"] = new SelectList(_context.Difficulties, "Id", "Id", game.DifficultyId);
            ViewData["EditorId"] = new SelectList(_context.Editors, "Id", "Name", game.EditorId);
            ViewData["IllustratorId"] = new SelectList(_context.Illustrators, "Id", "LastName", game.IllustratorId);
            return View(game);
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
