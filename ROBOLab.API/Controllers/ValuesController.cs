using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ROBOLab.Core.Models;
using ROBOLab.API;
using ROBOLab.Core.DTO;
using AutoMapper;

namespace ROBOLab.API.Controllers
{
    [Route("api/value")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ROBOLabDbContext _context;
        private readonly IMapper _mapper;

        public ValuesController(ROBOLabDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Value>>> GetValues()
        {
            return await _context.Values.ToListAsync();
        }

        // GET: api/value/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewDeviceValueDTO>> GetValue(int id)
        {
            var value = await _context.Values.Include(v => v.Device).Include(v => v.Property).Where(v => v.Id == id).FirstOrDefaultAsync();

            if (value == null)
            {
                return NotFound($"There is no value for given id: {id}.");
            }

            ViewDeviceValueDTO viewValueDTO = _mapper.Map<ViewDeviceValueDTO>(value);
            return Ok(viewValueDTO);
        }

        // PUT: api/Values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutValue(int id, Value value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }

            _context.Entry(value).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValueExists(id))
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

        // POST: api/Values
        [HttpPost]
        public async Task<ActionResult<Value>> PostValue(Value value)
        {
            _context.Values.Add(value);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetValue", new { id = value.Id }, value);
        }

        // DELETE: api/Values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Value>> DeleteValue(int id)
        {
            var value = await _context.Values.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }

            _context.Values.Remove(value);
            await _context.SaveChangesAsync();

            return value;
        }

        private bool ValueExists(int id)
        {
            return _context.Values.Any(e => e.Id == id);
        }
    }
}
