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
    public class MaterialSupportsController : Controller
    {
        private readonly LudothequeAccountContext _context;

        public MaterialSupportsController(LudothequeAccountContext context)
        {
            _context = context;
        }

        // GET: MaterialSupports
        public async Task<IActionResult> Index()
        {
            return View(await _context.MaterialSupport.ToListAsync());
        }

        // GET: MaterialSupports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialSupport = await _context.MaterialSupport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materialSupport == null)
            {
                return NotFound();
            }

            return View(materialSupport);
        }

        // GET: MaterialSupports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaterialSupports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] MaterialSupport materialSupport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materialSupport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(materialSupport);
        }

        // GET: MaterialSupports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialSupport = await _context.MaterialSupport.FindAsync(id);
            if (materialSupport == null)
            {
                return NotFound();
            }
            return View(materialSupport);
        }

        // POST: MaterialSupports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] MaterialSupport materialSupport)
        {
            if (id != materialSupport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materialSupport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialSupportExists(materialSupport.Id))
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
            return View(materialSupport);
        }

        // GET: MaterialSupports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialSupport = await _context.MaterialSupport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materialSupport == null)
            {
                return NotFound();
            }

            return View(materialSupport);
        }

        // POST: MaterialSupports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materialSupport = await _context.MaterialSupport.FindAsync(id);
            _context.MaterialSupport.Remove(materialSupport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialSupportExists(int id)
        {
            return _context.MaterialSupport.Any(e => e.Id == id);
        }
    }
}
