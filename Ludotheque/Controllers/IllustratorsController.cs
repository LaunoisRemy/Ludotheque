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
    public class IllustratorsController : Controller
    {
        private readonly LudothequeAccountContext _context;

        public IllustratorsController(LudothequeAccountContext context)
        {
            _context = context;
        }

        // GET: Illustrators
        public async Task<IActionResult> Index()
        {
            return View(await _context.Illustrators.ToListAsync());
        }

        // GET: Illustrators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var illustrator = await _context.Illustrators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (illustrator == null)
            {
                return NotFound();
            }

            return View(illustrator);
        }

        // GET: Illustrators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Illustrators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] Illustrator illustrator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(illustrator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(illustrator);
        }

        // GET: Illustrators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var illustrator = await _context.Illustrators.FindAsync(id);
            if (illustrator == null)
            {
                return NotFound();
            }
            return View(illustrator);
        }

        // POST: Illustrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] Illustrator illustrator)
        {
            if (id != illustrator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(illustrator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IllustratorExists(illustrator.Id))
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
            return View(illustrator);
        }

        // GET: Illustrators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var illustrator = await _context.Illustrators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (illustrator == null)
            {
                return NotFound();
            }

            return View(illustrator);
        }

        // POST: Illustrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var illustrator = await _context.Illustrators.FindAsync(id);
            _context.Illustrators.Remove(illustrator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IllustratorExists(int id)
        {
            return _context.Illustrators.Any(e => e.Id == id);
        }
    }
}
