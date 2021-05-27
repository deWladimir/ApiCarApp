﻿using System;
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
    public class FuelTypesController : ControllerBase
    {
        private readonly CarsContext _context;

        public FuelTypesController(CarsContext context)
        {
            _context = context;
        }

        // GET: api/FuelTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuelType>>> GetFuelTypes()
        {
            var fueltypes = await _context.FuelTypes.ToListAsync();
            
            return fueltypes;
        }

        // GET: api/FuelTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FuelType>> GetFuelType(int id)
        {
            var fuelType = await _context.FuelTypes.FindAsync(id);

            if (fuelType == null)
            {
                return NotFound();
            }

            return fuelType;
        }

        // PUT: api/FuelTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuelType(int id, FuelType fuelType)
        {
            if (id != fuelType.Id)
            {
                return BadRequest();
            }

            _context.Entry(fuelType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuelTypeExists(id))
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

        // POST: api/FuelTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FuelType>> PostFuelType(FuelType fuelType)
        {
            _context.FuelTypes.Add(fuelType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFuelType", new { id = fuelType.Id }, fuelType);
        }

        // DELETE: api/FuelTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuelType(int id)
        {
            var fuelType = await _context.FuelTypes.FindAsync(id);
            if (fuelType == null)
            {
                return NotFound();
            }

            var thisAutos = _context.Automobiles.Where(obj => obj.FuelTypeId == id).ToList();

            foreach (var a in thisAutos)
            {
                var ac = _context.CategoriesAutomobiles.Where(obj => obj.AutomobileId == a.Id);

                foreach (var x in ac)
                {
                    _context.CategoriesAutomobiles.Remove(x);
                }

                _context.Automobiles.Remove(a);
            }

            _context.FuelTypes.Remove(fuelType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FuelTypeExists(int id)
        {
            return _context.FuelTypes.Any(e => e.Id == id);
        }
    }
}
