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
    [Route("api/[controller]")]
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

        // GET: api/DeviceTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceTypeDTO>>> GetDeviceTypes()
        {
            var deviceTypes = await _context.DeviceTypes.ToListAsync();

            if (deviceTypes == null)
            {
                return NotFound("There is no device types in database.");
            }

            List<DeviceTypeDTO> deviceTypesDTO = new List<DeviceTypeDTO>();
            foreach (DeviceType d in deviceTypes)
            {
                DeviceTypeDTO deviceTypeDTO = _mapper.Map<DeviceTypeDTO>(d);
                deviceTypesDTO.Add(deviceTypeDTO);
            }

            return deviceTypesDTO;
        }

        // GET: api/DeviceTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceTypeDTO>> GetDeviceType(int id)
        {
            var deviceType = await _context.DeviceTypes.FindAsync(id);

            if (deviceType == null)
            {
                return NotFound($"There is no device type for given id: {id}.");
            }

            DeviceTypeDTO deviceTypeDTO = _mapper.Map<DeviceTypeDTO>(deviceType);
            return deviceTypeDTO;
        }

        // GET: api/DeviceTypes/5/user/{userId}/devices
        [HttpGet("{id}/devices/userId={userId}")]
        public async Task<ActionResult<ICollection<DeviceAddDTO>>> GetAllDevicesByDeviceType(int id, int userId)
        {
            var deviceType = await _context.DeviceTypes.FindAsync(id);
            var usersDevices = await _context.Users.Include(n => n.Devices).Where(user => user.Id == userId).SelectMany(user => user.Devices).ToListAsync();
            var usersDevicesByDeviceType = usersDevices.Where(device => device.DeviceTypeId == id).ToList();

            if (usersDevicesByDeviceType == null)
            {
                return NotFound($"There is no devices for device type: {deviceType.Name} for user with given id: {userId}");
            }

            List<DeviceAddDTO> devicesByDeviceTypeDTO = new List<DeviceAddDTO>();
            foreach (Device d in usersDevicesByDeviceType)
            {
                DeviceAddDTO deviceDTO = _mapper.Map<DeviceAddDTO>(d);
                devicesByDeviceTypeDTO.Add(deviceDTO);
            }

            return devicesByDeviceTypeDTO;
        }

        // PUT: api/DeviceTypes/5
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

            deviceTypeToUpdate.Name = deviceTypeDTO.DeviceTypeName;

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

        // POST: api/DeviceTypes
        [HttpPost]
        public async Task<ActionResult<DeviceType>> PostDeviceType(DeviceTypeDTO deviceTypeDTO)
        {
            if (DeviceTypeNameExists(deviceTypeDTO.DeviceTypeName))
            {
                return BadRequest("Device type with this name already exists.");
            }

            DeviceType deviceType = _mapper.Map<DeviceType>(deviceTypeDTO);
            _context.DeviceTypes.Add(deviceType);
            await _context.SaveChangesAsync();

            DeviceTypeDTO deviceTypeToView = _mapper.Map<DeviceTypeDTO>(deviceType);
            return CreatedAtAction("GetDeviceType", new { id = deviceTypeToView.Id }, deviceTypeToView);
        }

        //POST: /api/DeviceTypes/{id}/properties
        [HttpPost("{id}/properties")]
        public async Task<ActionResult<PropertyDTO>> PostPropertyToDeviceType(int id, PropertyDTO propertyDTO)
        {
            var deviceType = await _context.DeviceTypes.FindAsync(id);

            if (deviceType == null)
            {
                return NotFound($"There is no device type for given id: {id}.");
            }

            Property property = _mapper.Map<Property>(propertyDTO);

            await _context.Properties.AddAsync(property);
            deviceType.Properties.Add(property);
            await _context.SaveChangesAsync();

            return propertyDTO;
        }

        // DELETE: api/DeviceTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeviceType>> DeleteDeviceType(int id)
        {
            var deviceType = await _context.DeviceTypes.FindAsync(id);
            if (deviceType == null)
            {
                return NotFound($"There is no device type for given id: {id}.");
            }

            _context.DeviceTypes.Remove(deviceType);
            await _context.SaveChangesAsync();

            return deviceType;
        }

        private bool DeviceTypeExists(int id)
        {
            return _context.DeviceTypes.Any(e => e.Id == id);
        }

        private bool DeviceTypeNameExists(string name)
        {
            return _context.DeviceTypes.Any(d => d.Name == name);
        }
    }
}
