using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ludotheque.Areas.Identity.Data;
using Ludotheque.Data;
using Ludotheque.Models;
using Ludotheque.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ludotheque.Controllers
{
    [Authorize]
    public class MyGamesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public readonly UserManager<LudothequeUser> userManager;
        private readonly LudothequeAccountContext _context;
        private GamesService _gameService;
        private GameAllDataService _gameAllDataService;



        public MyGamesController(LudothequeAccountContext context, RoleManager<IdentityRole> roleManager, UserManager<LudothequeUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _context = context;
            _gameService = new GamesService(context);
            _gameAllDataService = new GameAllDataService(context);

        }

        public async Task<IActionResult> Index(string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            LudothequeUser user = await UserServices.GetUserAsync(userManager, User.Identity.Name);

            IQueryable<Game> games;

            games = _gameService.GetGamesByUser(user.Id);
            if (!String.IsNullOrEmpty(searchString))
            {
                games = _gameService.GetGamesByName(searchString,games);
                pageNumber = 1;
            }
            else 
            { 
                searchString = currentFilter;
            }
            games = _gameService.GetGamesValidate(games);
            GamesIndexData gamesAllData = await SortGames(searchString, sortOrder, currentFilter, pageNumber, games);

            ViewBag.MyGames = "true";
            return View(gamesAllData);
        }

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

            return await _gameAllDataService.SortGamesIndex(games, pageNumber, sortOrder);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGame(int id)
        {
            LudothequeUser user = await UserServices.GetUserAsync(userManager, User.Identity.Name);


            var gameUser = _context.GamesUser.Single(s => s.GameId == id && s.LudothequeUserId.Equals(user.Id) );

            if (gameUser == null)
            {
                return NotFound();
            }
            else
            {
                var result =  _context.GamesUser.Remove(gameUser);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGame(int id)
        {
            LudothequeUser user = await UserServices.GetUserAsync(userManager, User.Identity.Name);


            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }
            else
            {
                bool notFIndGame = _context.GamesUser.Single(s => s.GameId == id && s.LudothequeUserId.Equals(user.Id)) == null;
                GamesUser gamesUser = new GamesUser
                {
                    Game = game,
                    GameId = game.Id,
                    User = user,
                    LudothequeUserId = user.Id
                };

                if (ModelState.IsValid)
                {
                    if (notFIndGame)
                    {
                        var result = await _context.AddAsync(gamesUser);

                        //var result = _context.GamesUser.AddAsync(gamesUser);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index), "Games");

                    }
                    else
                    {
                        TempData["message"] = "Le jeu existe déja dans votre ludotheque";
                        return RedirectToAction(nameof(Index), "Games");
                    }

                }
                return NotFound();


            }
        }
    }
}