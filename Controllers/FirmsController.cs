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
    public class FirmsController : ControllerBase
    {
        private readonly CarsContext _context;

        public FirmsController(CarsContext context)
        {
            _context = context;
        }

        // GET: api/Firms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Firm>>> GetFirms()
        {
            var firms = await _context.Firms.ToListAsync();
           
            return firms;
        }

        // GET: api/Firms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Firm>> GetFirm(int id)
        {
            var firm = await _context.Firms.FindAsync(id);

            if (firm == null)
            {
                return NotFound();
            }

            
            return firm;
        }

        // PUT: api/Firms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFirm(int id, Firm firm)
        {
            if (id != firm.Id)
            {
                return BadRequest();
            }

            _context.Entry(firm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FirmExists(id))
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

        // POST: api/Firms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<string>> PostFirm(Firm firm)
        {
            var country = _context.Countries.Where(obj => obj.Id == firm.CountryId).FirstOrDefault();
            firm.Country = country;
            _context.Firms.Add(firm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFirm", new { id = firm.Id }, firm);
        }

        // DELETE: api/Firms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFirm(int id)
        {
            var firm = await _context.Firms.FindAsync(id);
            if (firm == null)
            {
                return NotFound();
            }

            var thisAutos = _context.Automobiles.Where(obj => obj.FirmId == id).ToList();

            foreach (var a in thisAutos)
            {

                var ac = _context.CategoriesAutomobiles.Where(obj => obj.AutomobileId == a.Id);

                foreach (var x in ac)
                {
                    _context.CategoriesAutomobiles.Remove(x);
                }

                _context.Automobiles.Remove(a);
            }

            _context.Firms.Remove(firm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FirmExists(int id)
        {
            return _context.Firms.Any(e => e.Id == id);
        }
    }
}
