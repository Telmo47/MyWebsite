﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Data;
using MyWebsite.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace MyWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProjectsController : Controller
    {

        /// <summary>
        /// Reference to the application database context
        /// </summary>
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }


        // GET: Projects
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projects == null)
            {
                return NotFound();
            }

            return View(projects);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,UrlGithub,UrlSite,ImageUrl,CreationDate")] Projects projects)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projects);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projects);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = await _context.Projects.FindAsync(id);
            if (projects == null)
            {
                return NotFound();
            }
            return View(projects);

            // Saves the data of the object in the session
            HttpContext.Session.SetInt32("ProjectId", projects.Id);
            HttpContext.Session.SetString("ProjectTitle", projects.Title);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        //Uses the [FromRoute] attribute to bind the id parameter from the route data
        public async Task<IActionResult> Edit([FromRoute] int id, [Bind("Id,Title,Description,UrlGithub,UrlSite,ImageUrl,CreationDate")] Projects projects)
        {
            if (id != projects.Id)
            {
                return RedirectToAction("Index");
            }

            // Are the data I received from the session corresponding to the object sent to the browser?
            var ProjectId = HttpContext.Session.GetInt32("ProjectId");
            var ProjectTitle = HttpContext.Session.GetString("ProjectTitle");

            if (ProjectId == null || ProjectTitle.IsNullOrEmpty())
            {
                // Took too long to get the data from the session, or the session has expired
                ModelState.AddModelError(string.Empty, "Project ID not found in session." + "You must restart the process");
                
                return View(projects);
            }

            // Was there any adultation of the object?
            if (projects.Id != ProjectId || ProjectTitle != "Projects/Edit")
            {
                // The user is trying to edit a different project than the one that was loaded in the session
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Saves the data in the database
                    _context.Update(projects);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectsExists(projects.Id))
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
            return View(projects);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projects == null)
            {
                return NotFound();
            }

            // Saves the data of the object in the session
            HttpContext.Session.SetInt32("ProjectId", projects.Id);
            HttpContext.Session.SetString("ProjectTitle", projects.Title);

            return View(projects);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projects = await _context.Projects.FindAsync(id);
            var ProjectId = HttpContext.Session.GetInt32("ProjectId");
            var ProjectTitle = HttpContext.Session.GetString("ProjectTitle");

            // Are the data I received from the session corresponding to the object sent to the browser?
            if (ProjectId == null || ProjectTitle.IsNullOrEmpty())
            {
                // Took too long to get the data from the session, or the session has expired
                ModelState.AddModelError(string.Empty, "Project ID not found in session." + "You must restart the process");
                return View(projects);
            }

            if (projects.Id != ProjectId || ProjectTitle != "Projects/Delete")
            {
                // The user is trying to delete a different project than the one that was loaded in the session
                return RedirectToAction("Index");
            }

            if (projects != null)
            {
                _context.Projects.Remove(projects);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectsExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
