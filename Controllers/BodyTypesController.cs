using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiCarApp.Models;

namespace ApiCarApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyTypesController : ControllerBase
    {
        private readonly CarsContext _context;

        public BodyTypesController(CarsContext context)
        {
            _context = context;
        }

        // GET: api/BodyTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BodyType>>> GetBodyTypes()
        {
            var bodytypes = await _context.BodyTypes.ToListAsync();
            foreach (var b in bodytypes)
            {
                b.Automobiles = _context.Automobiles.Where(obj => obj.BodyTypeId == b.Id).ToList();
            }
            return bodytypes;
        }

        // GET: api/BodyTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BodyType>> GetBodyType(int id)
        {
            var bodyType = await _context.BodyTypes.FindAsync(id);

            if (bodyType == null)
            {
                return NotFound();
            }

            bodyType.Automobiles = _context.Automobiles.Where(obj => obj.BodyTypeId == bodyType.Id).ToList();

            return bodyType;
        }

        // PUT: api/BodyTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBodyType(int id, BodyType bodyType)
        {
            if (id != bodyType.Id)
            {
                return BadRequest();
            }

            _context.Entry(bodyType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BodyTypeExists(id))
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

        // POST: api/BodyTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BodyType>> PostBodyType(BodyType bodyType)
        {
            _context.BodyTypes.Add(bodyType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBodyType", new { id = bodyType.Id }, bodyType);
        }

        // DELETE: api/BodyTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBodyType(int id)
        {
            var bodyType = await _context.BodyTypes.FindAsync(id);
            if (bodyType == null)
            {
                return NotFound();
            }

            var thisAutos = _context.Automobiles.Where(obj => obj.BodyTypeId == id).ToList();

            foreach (var a in thisAutos)
            {
                var ac = _context.CategoriesAutomobiles.Where(obj => obj.AutomobileId == a.Id);

                foreach (var x in ac)
                {
                    _context.CategoriesAutomobiles.Remove(x);
                }

                _context.Automobiles.Remove(a);
            }

            _context.BodyTypes.Remove(bodyType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BodyTypeExists(int id)
        {
            return _context.BodyTypes.Any(e => e.Id == id);
        }
    }
}
