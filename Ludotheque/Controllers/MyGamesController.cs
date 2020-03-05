using System;
using System.Collections.Generic;
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

        public async Task<IActionResult> IndexAsync(string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            var u = await userManager.FindByEmailAsync(User.Identity.Name);
            IQueryable<Game> games;
            var userTmp = await userManager.FindByIdAsync(u.Id);
            games = _gameService.GetGamesByUser(u.Id);
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

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var gameUser = _context.GamesUser.Single(s => s.User.Id.Equals(id));

            if (gameUser == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListRoles");
            }
        }*/
    }
}