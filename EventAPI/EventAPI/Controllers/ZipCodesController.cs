﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventAPI.Data;
using EventAPI.Model;

namespace EventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZipCodesController : ControllerBase
    {
        private readonly EventContext _context;

        public ZipCodesController(EventContext context)
        {
            _context = context;
        }

        // GET: api/ZipCodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZipCode>>> GetZipCodes()
        {
            return await _context.ZipCodes.ToListAsync();
        }

        // GET: api/ZipCodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZipCode>> GetZipCode(int id)
        {
            var zipCode = await _context.ZipCodes.FindAsync(id);

            if (zipCode == null)
            {
                return NotFound();
            }

            return zipCode;
        }

        // PUT: api/ZipCodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZipCode(int id, ZipCode zipCode)
        {
            if (id != zipCode.ZipCodeId)
            {
                return BadRequest();
            }

            _context.Entry(zipCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZipCodeExists(id))
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

        // POST: api/ZipCodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ZipCode>> PostZipCode(ZipCode zipCode)
        {
            _context.ZipCodes.Add(zipCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZipCode", new { id = zipCode.ZipCodeId }, zipCode);
        }

        // DELETE: api/ZipCodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZipCode(int id)
        {
            var zipCode = await _context.ZipCodes.FindAsync(id);
            if (zipCode == null)
            {
                return NotFound();
            }

            _context.ZipCodes.Remove(zipCode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ZipCodeExists(int id)
        {
            return _context.ZipCodes.Any(e => e.ZipCodeId == id);
        }
    }
}