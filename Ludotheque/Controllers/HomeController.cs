using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ludotheque.Models;
using Microsoft.AspNetCore.Authorization;
using Ludotheque.Data;

namespace Ludotheque.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly LudothequeAccountContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, LudothequeAccountContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            return View(_context.Games.ToList());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public JsonResult GetSearching(string SearchBy,string SearchValue)
        {
            List<Game> gamesList = new List<Game>();
            if(SearchValue == null)
            {
                gamesList = gamesList = _context.Games.ToList();

            }
            else
            {
                switch (SearchBy)
                {
                    case "Name":
                        gamesList = _context.Games.Where(s => s.Name.Contains(SearchValue)).ToList();
                        break;
                    case "MinPlayer":
                    case "MaxPlayer":
                    case "MinAge":
                    case "GameTime":
                        try
                        {
                            int val = Convert.ToInt32(SearchValue);
                            gamesList = SearchBy switch
                            {
                                "MinPlayer" => _context.Games.Where(s => s.MinPlayer == val).ToList(),
                                "MaxPlayer" => _context.Games.Where(s => s.MaxPlayer == val).ToList(),
                                "MinAge" => _context.Games.Where(s => s.MinimumAge == val).ToList(),
                                "GameTime" => _context.Games.Where(s => s.GameTime == val).ToList(),
                                _ => gamesList = _context.Games.ToList(),
                            };
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ce n'est pas un age : ", SearchValue);
                        }
                        break;
                    default:
                        gamesList = gamesList = _context.Games.ToList();
                        break;

                }
            }

            return Json(gamesList);
          
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
