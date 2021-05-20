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
    public class AutomobilesController : ControllerBase
    {
        private readonly CarsContext _context;

        public AutomobilesController(CarsContext context)
        {
            _context = context;
        }

        // GET: api/Automobiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Automobile>>> GetAutomobiles()
        {
            var automobiles = await _context.Automobiles.ToListAsync();

            foreach (var a in automobiles)
            {
                a.BodyType = await _context.BodyTypes.FindAsync(a.BodyTypeId);
                a.FuelType = await _context.FuelTypes.FindAsync(a.FuelTypeId);
                a.Firm = await _context.Firms.FindAsync(a.FirmId);
            }

            return automobiles;
        }

        // GET: api/Automobiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Automobile>> GetAutomobile(int id)
        {
            var automobile = await _context.Automobiles.FindAsync(id);

            if (automobile == null)
            {
                return NotFound();
            }

            automobile.BodyType = await _context.BodyTypes.FindAsync(automobile.BodyTypeId);
            automobile.FuelType = await _context.FuelTypes.FindAsync(automobile.FuelTypeId);
            automobile.Firm = await _context.Firms.FindAsync(automobile.FirmId);

            return automobile;
        }

        // PUT: api/Automobiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutomobile(int id, Automobile automobile)
        {
            if (id != automobile.Id)
            {
                return BadRequest();
            }

            _context.Entry(automobile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutomobileExists(id))
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

        // POST: api/Automobiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Automobile>> PostAutomobile(Automobile automobile)
        {
            _context.Automobiles.Add(automobile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAutomobile", new { id = automobile.Id }, automobile);
        }

        // DELETE: api/Automobiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutomobile(int id)
        {
            var automobile = await _context.Automobiles.FindAsync(id);
            if (automobile == null)
            {
                return NotFound();
            }

            var ac = _context.CategoriesAutomobiles.Where(obj => obj.AutomobileId == id);

            foreach (var x in ac)
            {
                _context.CategoriesAutomobiles.Remove(x);
            }

            _context.Automobiles.Remove(automobile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AutomobileExists(int id)
        {
            return _context.Automobiles.Any(e => e.Id == id);
        }
    }
}
