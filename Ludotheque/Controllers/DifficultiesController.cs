﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ludotheque.Data;
using Ludotheque.Models;
using Microsoft.AspNetCore.Authorization;

namespace Ludotheque.Controllers
{
    /// <summary>
    /// Controller of difficulties
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class DifficultiesController : Controller
    {
        //private readonly LudothequeContext _context;
        private readonly LudothequeAccountContext _context;

        public DifficultiesController(LudothequeAccountContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Difficulties
        public async Task<IActionResult> Index()
        {
            return View(await _context.Difficulties.ToListAsync());
        }

        // GET: Difficulties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var difficulty = await _context.Difficulties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (difficulty == null)
            {
                return NotFound();
            }

            return View(difficulty);
        }

        // GET: Difficulties/Create
        public IActionResult Create()
        {
            ViewData["Difficulty"] = new SelectList(_context.Difficulties, "Id", "label");
            return View();
        }

        // POST: Difficulties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,label")] Difficulty difficulty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(difficulty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(difficulty);
        }

        // GET: Difficulties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var difficulty = await _context.Difficulties.FindAsync(id);
            if (difficulty == null)
            {
                return NotFound();
            }
            ViewData["Difficulty"] = new SelectList(_context.Difficulties, "Id", "label");
            return View(difficulty);
        }

        // POST: Difficulties/Edit/
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,label")] Difficulty difficulty)
        {
            if (id != difficulty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(difficulty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DifficultyExists(difficulty.Id))
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
            return View(difficulty);
        }

        // GET: Difficulties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var difficulty = await _context.Difficulties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (difficulty == null)
            {
                return NotFound();
            }

            return View(difficulty);
        }

        // POST: Difficulties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var difficulty = await _context.Difficulties.FindAsync(id);
            _context.Difficulties.Remove(difficulty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DifficultyExists(int id)
        {
            return _context.Difficulties.Any(e => e.Id == id);
        }
    }
}
