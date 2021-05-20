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
    public class CategoriesAutomobilesController : ControllerBase
    {
        private readonly CarsContext _context;

        public CategoriesAutomobilesController(CarsContext context)
        {
            _context = context;
        }

        // GET: api/CategoriesAutomobiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriesAutomobiles>>> GetCategoriesAutomobiles()
        {
            return await _context.CategoriesAutomobiles.ToListAsync();
        }

        // GET: api/CategoriesAutomobiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriesAutomobiles>> GetCategoriesAutomobiles(int id)
        {
            var categoriesAutomobiles = await _context.CategoriesAutomobiles.FindAsync(id);

            if (categoriesAutomobiles == null)
            {
                return NotFound();
            }

            return categoriesAutomobiles;
        }

        // PUT: api/CategoriesAutomobiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriesAutomobiles(int id, CategoriesAutomobiles categoriesAutomobiles)
        {
            if (id != categoriesAutomobiles.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoriesAutomobiles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesAutomobilesExists(id))
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

        // POST: api/CategoriesAutomobiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriesAutomobiles>> PostCategoriesAutomobiles(CategoriesAutomobiles categoriesAutomobiles)
        {
            _context.CategoriesAutomobiles.Add(categoriesAutomobiles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriesAutomobiles", new { id = categoriesAutomobiles.Id }, categoriesAutomobiles);
        }

        // DELETE: api/CategoriesAutomobiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriesAutomobiles(int id)
        {
            var categoriesAutomobiles = await _context.CategoriesAutomobiles.FindAsync(id);
            if (categoriesAutomobiles == null)
            {
                return NotFound();
            }

            _context.CategoriesAutomobiles.Remove(categoriesAutomobiles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriesAutomobilesExists(int id)
        {
            return _context.CategoriesAutomobiles.Any(e => e.Id == id);
        }
    }
}
