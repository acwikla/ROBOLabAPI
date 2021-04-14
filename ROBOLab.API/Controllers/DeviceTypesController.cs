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
    [Route("api/device-types")]
    [ApiController]
    public class DeviceTypesController : ControllerBase
    {
        private readonly ROBOLabDbContext _context;
        private readonly IMapper _mapper;

        public DeviceTypesController(ROBOLabDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/device-types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceTypeDTO>>> GetDeviceTypes()
        {
            var deviceTypes = await _context.DeviceTypes.ToListAsync();

            return _mapper.Map<List<DeviceTypeDTO>>(deviceTypes);
        }

        // GET: api/device-types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceTypeDTO>> GetDeviceType(int id)
        {
            var deviceType = await _context.DeviceTypes.FindAsync(id);

            if (deviceType == null)
            {
                return NotFound($"There is no device type for given id: {id}.");
            }

            return _mapper.Map<DeviceTypeDTO>(deviceType);
        }

        // GET: api/device-types/{devTypeId}/devices/user/{userId}
        [HttpGet("{devTypeId}/devices/user/{userId}")]
        public async Task<ActionResult<IEnumerable<ViewDeviceDTO>>> GetAllDevicesByDeviceType(int devTypeId, int userId)
        {
            var devices = await _context.Devices
                .Where(d => d.UserId == userId && d.DeviceTypeId == devTypeId)
                .ToListAsync();

            return _mapper.Map<List<ViewDeviceDTO>>(devices);
        }

        // PUT: api/device-types/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceType(int id, DeviceTypeDTO deviceTypeDTO)
        {
            /*if (id != deviceType.Id)
            {
                return BadRequest();
            }*/

            var deviceTypeToUpdate = await _context.DeviceTypes.FindAsync(id);
            if (deviceTypeToUpdate == null)
            {
                return NotFound($"There is no device type for given id: {id}.");
            }

            deviceTypeToUpdate.Name = deviceTypeDTO.Name;

            _context.Entry(deviceTypeToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceTypeExists(id))
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

        // POST: api/device-types
        [HttpPost]
        public async Task<ActionResult<DeviceTypeDTO>> PostDeviceType(DeviceTypeDTO deviceTypeDTO)
        {
            if (DeviceTypeNameExists(deviceTypeDTO.Name))
            {
                return BadRequest("Device type with this name already exists.");
            }

            DeviceType deviceType = _mapper.Map<DeviceType>(deviceTypeDTO);
            _context.DeviceTypes.Add(deviceType);
            await _context.SaveChangesAsync();

            DeviceTypeDTO deviceTypeToView = _mapper.Map<DeviceTypeDTO>(deviceType);
            return CreatedAtAction("GetDeviceType", new { id = deviceTypeToView.Id }, deviceTypeToView);
        }

        //TODO: dodac metode, która przyjmuje deviceId i automatycznie doda do device.value 
        //wszystkie property jakie znajdują sie w device typie do ktorego nalezy device - albo dodac to do post/devtype/properties

        //POST: /api/device-types/{id}/properties
        [HttpPost("{id}/properties")]
        public async Task<ActionResult<ViewPropertyDTO>> PostPropertyToDeviceType(int id, AddPropertyDTO propertyDTO)
        { 
            var deviceType = await _context.DeviceTypes.FindAsync(id);

            if (deviceType == null)
            {
                return NotFound($"There is no device type for given id: {id}.");
            }

            if (PropertyNameExists(propertyDTO.Name))
            {
                return BadRequest($"Property with this name already exist for device type : {deviceType.Name}.");
            }

            Property property = _mapper.Map<Property>(propertyDTO);
            property.DeviceTypeId = deviceType.Id;
            property.DeviceType = deviceType;

            await _context.Properties.AddAsync(property);
            deviceType.Properties.Add(property);

            await _context.SaveChangesAsync();

            ViewPropertyDTO propertyToViewDTO = _mapper.Map<ViewPropertyDTO>(property);
            return propertyToViewDTO;
        }

        // DELETE: api/device-types/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeviceTypeDTO>> DeleteDeviceType(int id)
        {
            var deviceType = await _context.DeviceTypes.FindAsync(id);
            if (deviceType == null)
            {
                return NotFound($"There is no device type for given id: {id}.");
            }

            _context.DeviceTypes.Remove(deviceType);
            await _context.SaveChangesAsync();

            DeviceTypeDTO deviceTypeDTO = _mapper.Map<DeviceTypeDTO>(deviceType);
            return deviceTypeDTO;
        }

        private bool DeviceTypeExists(int id)
        {
            return _context.DeviceTypes.Any(e => e.Id == id);
        }

        private bool DeviceTypeNameExists(string name)
        {
            return _context.DeviceTypes.Any(d => d.Name == name);
        }

        private bool PropertyNameExists(string name)
        {

            var devTypeProperty = _context.DeviceTypes.Include(d => d.Properties).SelectMany(d => d.Properties).ToList();

            foreach (Property p in devTypeProperty)
            {
                if (p.Name==name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
