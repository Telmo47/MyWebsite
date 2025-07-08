using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Data;
using MyWebsite.Models;

namespace MyWebsite.Controllers
{
    public class TecnologiesController : Controller
    {

        /// <summary>
        /// Context for accessing the database
        /// </summary>
        private readonly AppDbContext _context;

        public TecnologiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tecnologies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tecnologies.ToListAsync());
        }

        // GET: Tecnologies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnologies = await _context.Tecnologies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tecnologies == null)
            {
                return NotFound();
            }

            return View(tecnologies);
        }

        // GET: Tecnologies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tecnologies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Tecnologies tecnologies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tecnologies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tecnologies);
        }

        // GET: Tecnologies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnologies = await _context.Tecnologies.FindAsync(id);
            if (tecnologies == null)
            {
                return NotFound();
            }
            return View(tecnologies);
        }

        // POST: Tecnologies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Tecnologies tecnologies)
        {
            if (id != tecnologies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tecnologies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TecnologiesExists(tecnologies.Id))
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
            return View(tecnologies);
        }

        // GET: Tecnologies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnologies = await _context.Tecnologies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tecnologies == null)
            {
                return NotFound();
            }

            return View(tecnologies);
        }

        // POST: Tecnologies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tecnologies = await _context.Tecnologies.FindAsync(id);
            if (tecnologies != null)
            {
                _context.Tecnologies.Remove(tecnologies);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TecnologiesExists(int id)
        {
            return _context.Tecnologies.Any(e => e.Id == id);
        }
    }
}
