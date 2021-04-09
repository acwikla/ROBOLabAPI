using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ROBOLab.Core.Models;
using ROBOLab.Core.DTO;
using ROBOLabAPI;
using AutoMapper;

namespace ROBOLabAPI.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly ROBOLabDbContext _context;
        private readonly IMapper _mapper;

        public DevicesController(ROBOLabDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/devices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewDeviceDTO>>> GetDevices()
        {
            var devices = await _context.Devices.Include(d => d.DeviceType).Include(d => d.User).ToListAsync();

            if (devices == null)
            {
                return NotFound("There is no devices in database.");
            }

            List<ViewDeviceDTO> devicesDTO = new List<ViewDeviceDTO>();
            foreach (Device d in devices)
            {
                ViewDeviceDTO deviceDTO = _mapper.Map<ViewDeviceDTO>(d);

                devicesDTO.Add(deviceDTO);
            }

            return Ok(devicesDTO);
        }

        // GET: api/devices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewDeviceDTO>> GetDevice(int id)
        {
            var device = await _context.Devices.Include(d => d.DeviceType).Include(d => d.User).Where(d => d.Id == id).FirstOrDefaultAsync();

            if (device == null)
            {
                return NotFound($"There is no device for given id: {id}.");
            }

            ViewDeviceDTO deviceDTO = _mapper.Map<ViewDeviceDTO>(device);
            return Ok(deviceDTO);
        }

        // PUT: api/devices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(int id, AddDeviceDTO deviceDTO)
        {
            /*if (id != device.Id)
            {
                return BadRequest();
            }*/
            var deviceToUpdate = await _context.Devices.FindAsync(id);

            if (deviceToUpdate == null)
            {
                return NotFound($"There is no device for given id: {id}.");
            }

            deviceToUpdate.Name = deviceDTO.DeviceName;

            _context.Entry(deviceToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
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

        //TODO: dodac metode, ktora pobiera wszystkie value dla konkretnego device
        //TODO: dodac metode, ktora pobiera wartosci dla danego urzadzenia, dla konkretnego property 

        // POST: api/devices/{id}/values
        [HttpPost("{id}/values")]
        public async Task<ActionResult<Device>> PostNewValueForDevice(int id, AddPropertyValueDTO propertyValueDTO)
        {
            //pobierz property z dev type i values, device z dev type i z values
            var device = await _context.Devices.Include(d => d.DeviceType).Include(d => d.Values).Where(d => d.Id == id).FirstOrDefaultAsync();
            if (device == null)
            {
                return NotFound($"There is no device for given id: {id}.");
            }

            //search by property name
            var property = await _context.Properties.Include(p => p.DeviceType).Where(p => p.DeviceType.Name == device.DeviceType.Name).Include(p => p.Values).Where(p=>p.Name==propertyValueDTO.Property.Name).FirstOrDefaultAsync();
            if (property == null)
            {
                //Nie istnieje takie property o podanej nazwie z pasujacym device type, dla podanego device
                return BadRequest($"There is no property named : {propertyValueDTO.Property.Name} with matching device type for device with given id: {id}.");
            }

            //sprawdz czy dev type w property zgadza się z dev type w device
            //na wszelki wypadek
            if (property.DeviceTypeId == device.DeviceTypeId)
            {
                return BadRequest($"Device types in property and device do not match.");
            }

            //znajdz value
            var value = device.Values.Where(v => v.PropertyId == property.Id).Where(v=>v.DeviceId==id).FirstOrDefault();
            if (value==null)
            {
                return BadRequest($"There is no value with matching poperty for device with given id: {id}");
            }

            //dodaj  values, dodaj values do listy z property, dodaj values do listy w device type
            //dodaj automatyczne generowanie daty 
            Value newValue = _mapper.Map<Value>(propertyValueDTO);
            await _context.Values.AddAsync(newValue);
            await _context.SaveChangesAsync();

            device.Values.Add(newValue);
            property.Values.Add(newValue);

            ViewDeviceValueDTO valueDTO = _mapper.Map<ViewDeviceValueDTO>(newValue);
            return CreatedAtAction("GetValues", new { id = valueDTO.Id }, valueDTO);
        }

        // DELETE: api/devices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ViewDeviceDTO>> DeleteDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound($"There is no device for given id: {id}.");
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            ViewDeviceDTO deviceDTO = _mapper.Map<ViewDeviceDTO>(device);
            return deviceDTO;
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
    }
}
