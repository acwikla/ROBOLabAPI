﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ROBOLab.Core.Models;
using ROBOLab.Core.DTO;
using ROBOLab.API;
using AutoMapper;

namespace ROBOLab.API.Controllers
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

        // GET: api/devices/values/5
        [HttpGet("values/{value_id}")]
        public async Task<ActionResult<ViewDeviceValueDTO>> GetDeviceValue(int value_id)
        {
            var value = await _context.Values.Include(v => v.Device).Include(v => v.Property).Where(v => v.Id == value_id).FirstOrDefaultAsync();

            if (value == null)
            {
                return NotFound($"There is no value for given id: {value_id}.");
            }

            ViewDeviceValueDTO viewValueDTO = _mapper.Map<ViewDeviceValueDTO>(value);
            return Ok(viewValueDTO);
        }

        // GET: api/devices/5/values/take-last/100
        [HttpGet("{id}/values/take-last/{amount}")]
        public async Task<ActionResult<IEnumerable<ViewDeviceValueDTO>>> GetDeviceLastValue(int id, int amount)
        {
            IEnumerable<Value> values = await _context.Values.Include(v => v.Device).Include(v => v.Property).Where(v => v.DeviceId == id).ToListAsync();

            if (values == null)
            {
                return NotFound($"There is no values for given device id: {id}.");
            }

            return _mapper.Map<List<ViewDeviceValueDTO>>(values.TakeLast(amount));
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

        // POST: api/devices/{id}/add-values-by-property-name
        [HttpPost("{id}/add-values-by-property-name")]
        public async Task<ActionResult<Device>> PostNewValueForDeviceByPropName(int id, AddPropertyValueDTO propertyValueDTO)
        {
            var device = await _context.Devices.Include(d => d.DeviceType).Where(d => d.Id == id).FirstOrDefaultAsync();
            if (device == null)
            {
                return NotFound($"There is no device for given id: {id}.");
            }

            //search by property name
            var property = await _context.Properties.Include(p => p.DeviceType).Where(p=>p.Name==propertyValueDTO.PropertyName).FirstOrDefaultAsync();
            if (property == null)
            {
                return BadRequest($"There is no property named : {propertyValueDTO.PropertyName} with matching device type for device with given id: {id}.");
            }

            //check if devtype in property and device are equal
            if (property.DeviceTypeId != device.DeviceType.Id)
            {
                return BadRequest($"Device types in property and device do not match.");
            }

            var newValue = new Value
            {
                Val = propertyValueDTO.Val,
                DateTime = DateTime.Now,
                PropertyId = property.Id,
                Property = property,
                DeviceId = device.Id,
                Device = device
            };
            await _context.Values.AddAsync(newValue);
            await _context.SaveChangesAsync();

            device.Values.Add(newValue);
            property.Values.Add(newValue);

            ViewDeviceValueDTO valueDTO = _mapper.Map<ViewDeviceValueDTO>(newValue);
            return CreatedAtAction(nameof(GetDeviceValue), new { value_id = valueDTO.Id }, valueDTO);
        }

        // POST: api/devices/{id}/add-values-by-property-id
        [HttpPost("{id}/add-values-by-property-id")]
        public async Task<ActionResult<ViewDeviceValueDTO>> PostNewValueForDeviceByPropId(int id, AddPropertyValueDTO propertyValueDTO)
        {
            var device = await _context.Devices.Include(d => d.DeviceType).Where(d => d.Id == id).FirstOrDefaultAsync();
            if (device == null)
            {
                return NotFound($"There is no device for given id: {id}.");
            }

            //search by property id
            var property = await _context.Properties.Include(p => p.DeviceType).Where(p => p.Id == propertyValueDTO.PropertyId).FirstOrDefaultAsync();
            if (property == null)
            {
                return BadRequest($"There is no property for given id: {propertyValueDTO.PropertyId}.");
            }

            //check if devtype in property and device are equal
            if (property.DeviceTypeId != device.DeviceType.Id)
            {
                return BadRequest($"Device types in property and device do not match.");
            }

            var newValue = new Value
            {
                Val = propertyValueDTO.Val,
                DateTime = DateTime.Now,
                PropertyId = property.Id,
                Property = property,
                DeviceId = device.Id,
                Device = device
            };
            await _context.Values.AddAsync(newValue);
            await _context.SaveChangesAsync();

            device.Values.Add(newValue);
            property.Values.Add(newValue);

            ViewDeviceValueDTO valueDTO = _mapper.Map<ViewDeviceValueDTO>(newValue);
            return CreatedAtAction(nameof(GetDeviceValue), new { value_id = valueDTO.Id }, valueDTO);
            //return CreatedAtAction("GetValue", new { id = valueDTO.Id }, valueDTO);
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
