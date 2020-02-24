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
    public class JeusController : Controller
    {
        private readonly LudothequeContext _context;

        public JeusController(LudothequeContext context)
        {
            _context = context;
        }

        // GET: Jeus
        public async Task<IActionResult> Index(string searchString)
        {
            //return View(await _context.Jeu.ToListAsync());
            var jeux = from j in _context.Jeu
                         select j;

            if (!String.IsNullOrEmpty(searchString))
            {
                jeux = jeux.Where(s => s.Nom.Contains(searchString));
            }
            //TODO: await jeux.ToListAsync a regarder ce que ca fait
            return View(await jeux.ToListAsync());
        }

        // GET: Jeus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jeu = await _context.Jeu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jeu == null)
            {
                return NotFound();
            }

            return View(jeu);
        }

        // GET: Jeus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jeus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,NbMinJoueur,NbMaxJoueur,AgeMinimum,TempsJeu,Prix,LienAchat,LienVideo,LienImg,Categorie")] Jeu jeu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jeu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jeu);
        }

        // GET: Jeus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jeu = await _context.Jeu.FindAsync(id);
            if (jeu == null)
            {
                return NotFound();
            }
            return View(jeu);
        }

        // POST: Jeus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,NbMinJoueur,NbMaxJoueur,AgeMinimum,TempsJeu,Prix,LienAchat,LienVideo,LienImg,Categorie")] Jeu jeu)
        {
            if (id != jeu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jeu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JeuExists(jeu.Id))
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
            return View(jeu);
        }

        // GET: Jeus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jeu = await _context.Jeu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jeu == null)
            {
                return NotFound();
            }

            return View(jeu);
        }

        // POST: Jeus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jeu = await _context.Jeu.FindAsync(id);
            _context.Jeu.Remove(jeu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JeuExists(int id)
        {
            return _context.Jeu.Any(e => e.Id == id);
        }
    }
}
