using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiMusicInfo.Data;
using apiMusicInfo.Models;
using apiMusicInfo.Controllers.Services;
namespace apiMusicInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {
        private readonly DataContext _context;

        public MusiciansController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Musicians
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Musician>>> GetMusicians()
        {
            var musicians = await _context.Musicians.ToListAsync();
            return Ok(musicians);
        }

        // GET: api/Musicians/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Musician>> GetMusician(string id)
        {
            var musician = await _context.Musicians.FindAsync(id);

            if (musician == null)
            {
                return NotFound();
            }

            return Ok(musician);
        }

        // POST: api/Musicians
        [HttpPost]
        public async Task<ActionResult<Musician>> PostMusician(Musician musician)
        {
            _context.Musicians.Add(musician);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMusician), new { id = musician.Name }, musician);
        }
    }
}