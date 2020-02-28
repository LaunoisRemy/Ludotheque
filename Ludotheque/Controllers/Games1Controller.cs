using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ludotheque.Data;
using Ludotheque.Models;

namespace Ludotheque.Controllers
{
    public class Games1Controller : Controller
    {
        private readonly LudothequeContext _context;

        public Games1Controller(LudothequeContext context)
        {
            _context = context;
        }

        // GET: Games1
        public async Task<IActionResult> Index()
        {
            var ludothequeContext = _context.Games.Include(g => g.Difficulty).Include(g => g.Editor).Include(g => g.Illustrator);
            return View(await ludothequeContext.ToListAsync());
        }

        // GET: Games1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.Difficulty)
                .Include(g => g.Editor)
                .Include(g => g.Illustrator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games1/Create
        public IActionResult Create()
        {
            ViewData["DifficultyId"] = new SelectList(_context.Difficulties, "Id", "Id");
            ViewData["EditorId"] = new SelectList(_context.Editors, "Id", "Name");
            ViewData["IllustratorId"] = new SelectList(_context.Illustrators, "Id", "LastName");
            return View();
        }

        // POST: Games1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,MinPlayer,MaxPlayer,MinimumAge,GameTime,Price,ReleaseDate,BuyLink,VideoLink,PictureLink,Validate,DifficultyId,IllustratorId,EditorId")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DifficultyId"] = new SelectList(_context.Difficulties, "Id", "Id", game.DifficultyId);
            ViewData["EditorId"] = new SelectList(_context.Editors, "Id", "Name", game.EditorId);
            ViewData["IllustratorId"] = new SelectList(_context.Illustrators, "Id", "LastName", game.IllustratorId);
            return View(game);
        }

        // GET: Games1/Edit/5
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
            ViewData["DifficultyId"] = new SelectList(_context.Difficulties, "Id", "Id", game.DifficultyId);
            ViewData["EditorId"] = new SelectList(_context.Editors, "Id", "Name", game.EditorId);
            ViewData["IllustratorId"] = new SelectList(_context.Illustrators, "Id", "LastName", game.IllustratorId);
            return View(game);
        }

        // POST: Games1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,MinPlayer,MaxPlayer,MinimumAge,GameTime,Price,ReleaseDate,BuyLink,VideoLink,PictureLink,Validate,DifficultyId,IllustratorId,EditorId")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

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

        // GET: Games1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.Difficulty)
                .Include(g => g.Editor)
                .Include(g => g.Illustrator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games1/Delete/5
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
