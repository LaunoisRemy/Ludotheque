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
using PagedList;
using Type = Ludotheque.Models.Type;

namespace Ludotheque.Controllers
{
    public class GamesController : Controller
    {
        private readonly LudothequeContext _context;
        private GamesService _gameService;

        public GamesController(LudothequeContext context)
        {
            _context = context;
            _gameService = new GamesService(context);

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
            ViewBag.CurrentFilter = searchString;
            
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

            games = _gameService.SortGames(games, sortOrder);
            IQueryable<GameAllData> gamesAllData= Enumerable.Empty<GameAllData>().AsQueryable();
            foreach (var game in games)
            {
                var idCategories = from relation in _context.GameCategories 
                    where game.Id == relation.GameId 
                    select relation.CategoryId;

                var categories = from c in _context.Categories
                    where idCategories.Contains(c.Id)
                    select c;

                var themesGame = categories.Where(s => s.Type == Type.Theme); 
                var MaterialSupportGame = categories.Where(s => s.Type == Type.MaterialSupport); 
                var MechanismGame = categories.Where(s => s.Type == Type.Mecanism); 

                var gameAllData = new GameAllData();
                gameAllData.ThemeCategoryList = themesGame;
                gameAllData.MaterialSupportCategoryList = MaterialSupportGame;
                gameAllData.MechanismCategoryList = MechanismGame;
                gamesAllData.Append(gameAllData);
            }
 

            int pageSize = 3;
            return View(await PaginatedList<Game>.CreateAsync(games.AsNoTracking(), pageNumber ?? 1, pageSize));
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
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,MinPlayer,MaxPlayer,MinimumAge,GameTime,Price,ReleaseDate,BuyLink,VideoLink,PictureLink,Validate")] Game game)
        {
            if (ModelState.IsValid)
            {
                await _gameService.AddGame(game);
                return RedirectToAction(nameof(Index));
            }
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
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,MinPlayer,MaxPlayer,MinimumAge,GameTime,Price,ReleaseDate,BuyLink,VideoLink,PictureLink,Validate")] Game game)
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
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.Id == id);
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
