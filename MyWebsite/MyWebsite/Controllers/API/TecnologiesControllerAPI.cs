using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Data;
using MyWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebsite.Controllers.API
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TecnologiesControllerAPI : ControllerBase
    {
        private readonly AppDbContext _context;
        public TecnologiesControllerAPI(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Tecnologies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tecnologies>>> GetTecnologies()
        {
            return await _context.Tecnologies
                .OrderBy(t => t.Id)
                .Select(t => new Tecnologies
                {
                    Name = t.Name,
                    ProjectTecnologies = t.ProjectTecnologies
                })
                .ToListAsync();
        }
        // GET: api/Tecnologies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tecnologies>> GetTecnology(int id)
        {
            var tecnology = await _context.Tecnologies.FindAsync(id);
            if (tecnology == null)
            {
                return NotFound();
            }
            return new Tecnologies
            {
                Name = tecnology.Name,
                ProjectTecnologies = tecnology.ProjectTecnologies
            };
        }
        // Put: api/Tecnologies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTecnology(int id, Tecnologies tecnology)
        {
            if (id != tecnology.Id)
            {
                return BadRequest();
            }
            _context.Entry(tecnology).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TecnologyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        // POST: api/Tecnologies
        [HttpPost]
        public async Task<ActionResult<Tecnologies>> PostTecnology(Tecnologies tecnology)
        {
            _context.Tecnologies.Add(tecnology);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTecnology", new { id = tecnology.Id }, tecnology);
        }
        // DELETE: api/Tecnologies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTecnology(int id)
        {
            var tecnology = await _context.Tecnologies.FindAsync(id);
            if (tecnology == null)
            {
                return NotFound();
            }
            _context.Tecnologies.Remove(tecnology);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool TecnologyExists(int id)
        {
            return _context.Tecnologies.Any(e => e.Id == id);
        }
    }
}

