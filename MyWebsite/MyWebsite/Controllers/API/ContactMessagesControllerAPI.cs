using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Data;
using MyWebsite.Models;

namespace MyWebsite.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactMessagesControllerAPI : ControllerBase
    {
        private readonly AppDbContext _context;
        public ContactMessagesControllerAPI(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/ContactMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactMessage>>> GetContactMessages()
        {
            return await _context.ContactMessages.ToListAsync();
        }
        // GET: api/ContactMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactMessage>> GetContactMessage(int id)
        {
            var contactMessage = await _context.ContactMessages.FindAsync(id);
            if (contactMessage == null)
            {
                return NotFound();
            }
            return contactMessage;
        }
        // POST: api/ContactMessages
        [HttpPost]
        public async Task<ActionResult<ContactMessage>> PostContactMessage(ContactMessage contactMessage)
        {
            _context.ContactMessages.Add(contactMessage);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetContactMessage", new { id = contactMessage.Id }, contactMessage);
        }

        // PUT: api/ContactMessages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactMessage(int id, ContactMessage contactMessage)
        {
            if (id != contactMessage.Id)
            {
                return BadRequest();
            }
            _context.Entry(contactMessage).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactMessageExists(id))
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

        // DELETE: api/ContactMessages/5
        [HttpDelete("{id}")]
        public ActionResult DeleteContactMessage(int id)
        {
            var contactMessage = _context.ContactMessages.Find(id);
            if (contactMessage == null)
            {
                return NotFound();
            }
            _context.ContactMessages.Remove(contactMessage);
            _context.SaveChanges();
            return NoContent();
        }
        private bool ContactMessageExists(int id)
        {
            return _context.ContactMessages.Any(e => e.Id == id);
        }
    }
}
