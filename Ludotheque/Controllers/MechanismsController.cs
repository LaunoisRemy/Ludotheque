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
    public class MechanismsController : Controller
    {
        private readonly LudothequeAccountContext _context;

        public MechanismsController(LudothequeAccountContext context)
        {
            _context = context;
        }

        // GET: Mechanisms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mechanism.ToListAsync());
        }

        // GET: Mechanisms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mechanism = await _context.Mechanism
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mechanism == null)
            {
                return NotFound();
            }

            return View(mechanism);
        }

        // GET: Mechanisms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mechanisms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Mechanism mechanism)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mechanism);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mechanism);
        }

        // GET: Mechanisms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mechanism = await _context.Mechanism.FindAsync(id);
            if (mechanism == null)
            {
                return NotFound();
            }
            return View(mechanism);
        }

        // POST: Mechanisms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Mechanism mechanism)
        {
            if (id != mechanism.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mechanism);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MechanismExists(mechanism.Id))
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
            return View(mechanism);
        }

        // GET: Mechanisms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mechanism = await _context.Mechanism
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mechanism == null)
            {
                return NotFound();
            }

            return View(mechanism);
        }

        // POST: Mechanisms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mechanism = await _context.Mechanism.FindAsync(id);
            _context.Mechanism.Remove(mechanism);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MechanismExists(int id)
        {
            return _context.Mechanism.Any(e => e.Id == id);
        }
    }
}
