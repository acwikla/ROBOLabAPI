using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ROBOLab.Core.DTO;
using ROBOLab.Core.Models;
using ROBOLab.API;

namespace ROBOLab.API.Controllers
{
    [Route("api/properties")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ROBOLabDbContext _context;
        private readonly IMapper _mapper;

        public PropertiesController(ROBOLabDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/properties/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ViewPropertyDTO>>> GetProperties()
        {
            var property = await _context.Properties.Include(p => p.DeviceType).ToListAsync();

            if (property == null)
            {
                return NotFound($"There is no property in database.");
            }

            List<ViewPropertyDTO> popertiesToViewDTO = new List<ViewPropertyDTO>();
            foreach (Property p in property)
            {
                ViewPropertyDTO propertyToViewDTO = _mapper.Map<ViewPropertyDTO>(p);
                popertiesToViewDTO.Add(propertyToViewDTO);
            }
            
            return popertiesToViewDTO;
        }

        // GET: api/properties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewPropertyDTO>> GetProperty(int id)
        {
            var property = await _context.Properties.Include(p => p.DeviceType).Where(p => p.Id == id).FirstOrDefaultAsync();

            if (property == null)
            {
                return NotFound($"There is no property with given id: {id}.");
            }

            ViewPropertyDTO propertyToViewDTO = _mapper.Map<ViewPropertyDTO>(property);

            return propertyToViewDTO;
        }

        // GET: api/properties?devtype={type}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewPropertyDTO>>> GetPropertyForDeviceType([FromQuery] string devtype)
        {
            var deviceType = await _context.DeviceTypes.Include(d => d.Properties).Where(deviceType => deviceType.Name == devtype).FirstOrDefaultAsync();

            if (deviceType == null)
            {
                return NotFound($"There is no device type with given name: {devtype}.");
            }

            if (deviceType.Properties == null)
            {
                return NotFound($"There is no properties for device type with given name: {devtype}.");
            }

            List<ViewPropertyDTO> propertiesToViewDTO = new List<ViewPropertyDTO>();
            foreach (Property p in deviceType.Properties)
            {
                ViewPropertyDTO propertyToViewDTO = _mapper.Map<ViewPropertyDTO>(p);
            }

            return propertiesToViewDTO;
        }

        // PUT: api/properties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProperty(int id, AddPropertyDTO propertyDTO)
        {
            /*if (id != property.Id)
            {
                return BadRequest();
            }*/

            var propertyToUpdate = await _context.Properties.Include(p => p.DeviceType).Where(p => p.Id == id).FirstOrDefaultAsync();

            if (propertyToUpdate == null)
            {
                return NotFound($"There is no property with given id: {id}.");
            }

            propertyToUpdate.Name = propertyDTO.Name;
            propertyToUpdate.Body = propertyDTO.Body;
            propertyToUpdate.IsMode = propertyDTO.IsMode;

            _context.Entry(propertyToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
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

        // DELETE: api/properties/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ViewPropertyDTO>> DeleteProperty(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound($"There is no property with given id: {id}.");
            }

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();

            ViewPropertyDTO propertyToViewDTO = _mapper.Map<ViewPropertyDTO>(property);
            return propertyToViewDTO;
        }

        private bool PropertyExists(int id)
        {
            return _context.Properties.Any(e => e.Id == id);
        }
    }
}
