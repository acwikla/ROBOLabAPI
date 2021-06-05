using System;
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
using ClosedXML.Excel;
using System.IO;

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
        [HttpGet("{id}/take-last-values/{amount}")]
        public async Task<ActionResult<IEnumerable<ViewDeviceValueDTO>>> GetDeviceLastValues(int id, int amount)
        {
            if (amount > 10000)
            {
                amount = 10000;
            }

            IEnumerable<Value> values = await _context.Values.Include(v => v.Device).Include(v => v.Property).Where(v => v.DeviceId == id)
                .OrderByDescending(v => v.DateTime).Take(amount).ToListAsync();

            //DB jesli takelast jest po toListAsync to: z bazy pobiora sie wszystkie wartosci, a dopiero pozniej z nich zwrocisz amount, i tak jest zle
            //to baza ma zwrocic ograniczona ilosc rekordow a wiec: takelast PRZED to list

            if (values == null)
            {
                return NotFound($"There is no values for given device id: {id}.");
            }

            return _mapper.Map<List<ViewDeviceValueDTO>>(values);
        }

        // GET: api/devices/5/export-last-values/100
        [HttpGet("{id}/export-last-values/{amount}")]
        public IActionResult ExportDeviceLastValues(int id, int amount)
        {
            if (amount > 10000)
            {
                amount = 10000;
            }

            IEnumerable<Value> values = _context.Values.Include(v => v.Device).Include(v => v.Property).Where(v => v.DeviceId == id)
                .OrderByDescending(v => v.DateTime).Take(amount).ToList();

            if (values == null)
            {
                return NotFound($"There is no values for given device id: {id}.");
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Device_Values");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Value";
                worksheet.Cell(currentRow, 2).Value = "DateTime";
                worksheet.Cell(currentRow, 3).Value = "Device Name";
                worksheet.Cell(currentRow, 4).Value = "Device ID";
                worksheet.Cell(currentRow, 5).Value = "Property Name";
                worksheet.Cell(currentRow, 6).Value = "Property ID";
                //worksheet.Cell(currentRow, 7).Value = "DeviceJobId";

                foreach (var v in values)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = v.Val;
                    worksheet.Cell(currentRow, 2).Value = v.DateTime;
                    worksheet.Cell(currentRow, 3).Value = v.Device.Name;
                    worksheet.Cell(currentRow, 4).Value = v.DeviceId;
                    worksheet.Cell(currentRow, 5).Value = v.Property.Name;
                    worksheet.Cell(currentRow, 6).Value = v.PropertyId;
                    //worksheet.Cell(currentRow, 7).Value = v.DeviceJobId;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "values.xlsx");
                }
            }
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

            //DB: moze byc wiele propertisow o tej samej nazwie dla kilku urzadzen,
            // dlatego podczas podbierania propery nalezy od razu filtrowac po device type

            //search by property name
            var property = await _context.Properties.Include(p => p.DeviceType)
                .Where(p => p.Name == propertyValueDTO.PropertyName)
                .Where(p => p.DeviceTypeId == device.DeviceTypeId)           // <---- tego brakowalo
                .FirstOrDefaultAsync();
            if (property == null)
            {
                return BadRequest($"There is no property named : {propertyValueDTO.PropertyName} with matching device type for device with given id: {id}.");
            }

            //DB i wtedy to jest zbedne...

            // natomiast funkcja PostNewValueForDeviceByPropId jest OK :)

            //check if devtype in property and device are equal
            /*if (property.DeviceTypeId != device.DeviceType.Id)
            {
                return BadRequest($"Device types in property and device do not match.");
            }*/

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

            //DB: zakomentowalem, sprawdz czy nadal dziala (czy dane zapisuja sie do bazy)
            //device.Values.Add(newValue);
            //property.Values.Add(newValue);

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
                PropertyId = property.Id,
                Property = property,
                DeviceId = device.Id,
                Device = device
            };
            await _context.Values.AddAsync(newValue);
            await _context.SaveChangesAsync();

            //DB: zakomentowalem, sprawdz czy nadal dziala (czy dane zapisuja sie do bazy)
            //device.Values.Add(newValue);
            //property.Values.Add(newValue);

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
