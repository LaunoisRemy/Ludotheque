﻿using System;
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

        public GamesController(LudothequeContext context)
        {
            _context = context;
            _gameService = new GamesService(context);

        }

        // GET: Games
        public async Task<IActionResult> Index(string searchString)
        {
            //return View(await _context.Jeu.ToListAsync());
            IQueryable<Game> games;

            if (!String.IsNullOrEmpty(searchString))
            {
                games = _gameService.GetGamesByName(searchString);
            }
            else
            {
                games = _gameService.GetGames();
            }

            return View(await games.ToListAsync());
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
                _gameService.AddGame(game);
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
            //Todo : Erreur de validation a cause du 
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
