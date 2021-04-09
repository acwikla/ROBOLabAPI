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
using ROBOLabAPI;

namespace ROBOLabAPI.Controllers
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
        public async Task<ActionResult<IEnumerable<PropertyToViewDTO>>> GetProperties()
        {
            var property = await _context.Properties.Include(p => p.DeviceType).ToListAsync();

            if (property == null)
            {
                return NotFound($"There is no property in database.");
            }

            List<PropertyToViewDTO> popertiesToViewDTO = new List<PropertyToViewDTO>();
            foreach (Property p in property)
            {
                PropertyToViewDTO propertyToViewDTO = _mapper.Map<PropertyToViewDTO>(property);
                popertiesToViewDTO.Add(propertyToViewDTO);
            }
            
            return popertiesToViewDTO;
        }

        // GET: api/properties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyToViewDTO>> GetProperty(int id)
        {
            var property = await _context.Properties.Include(p => p.DeviceType).Where(p => p.Id == id).FirstOrDefaultAsync();

            if (property == null)
            {
                return NotFound($"There is no property with given id: {id}.");
            }

            PropertyToViewDTO propertyToViewDTO = _mapper.Map<PropertyToViewDTO>(property);

            return propertyToViewDTO;
        }

        // GET: api/properties?devtype={type}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyToViewDTO>>> GetPropertyForDeviceType([FromQuery] string devtype)
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

            List<PropertyToViewDTO> propertiesToViewDTO = new List<PropertyToViewDTO>();
            foreach (Property p in deviceType.Properties)
            {
                PropertyToViewDTO propertyToViewDTO = _mapper.Map<PropertyToViewDTO>(p);
            }

            return propertiesToViewDTO;
        }

        // PUT: api/properties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProperty(int id, PropertyAddDTO propertyDTO)
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
        public async Task<ActionResult<PropertyToViewDTO>> DeleteProperty(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound($"There is no property with given id: {id}.");
            }

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();

            PropertyToViewDTO propertyToViewDTO = _mapper.Map<PropertyToViewDTO>(property);
            return propertyToViewDTO;
        }

        private bool PropertyExists(int id)
        {
            return _context.Properties.Any(e => e.Id == id);
        }
    }
}
