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
using ClosedXML.Excel;
using System.IO;

namespace ROBOLab.API.Controllers
{
    [Route("api/device-jobs")]
    [ApiController]
    public class DeviceJobsController : ControllerBase
    {
        private readonly ROBOLabDbContext _context;
        private readonly IMapper _mapper;

        public DeviceJobsController(ROBOLabDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/device-jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewDeviceJobDTO>>> GetDeviceJobs()
        {
            //var deviceJobs = await _context.DeviceJobs.Include(d => d.Device).Include(d => d.Job).Include(d => d.Device.DeviceType).Include(d => d.Job.DeviceType).ToListAsync();
            //without include device type in job and device
            //var deviceJobs = await _context.DeviceJobs.Include(d => d.Device).Include(j => j.Job).ToListAsync();

            // te include'y nie sa konieczne bo jest wlaczony lazy loading, ALE zostawilem je bo i tak sie zainkluduja podczas mapowania,
            // ale bedzie to wtedy mniej wydajne -> ogolnie drobnostka :)
            var deviceJobs = await _context.DeviceJobs
                .Include(d => d.Job)
                    .ThenInclude(j => j.DeviceType)
                .ToListAsync();

            //ToList nie zwroci null tylko pusta liste, wiec nie bedzie takiego przypadku,
            //w dodatku to nie jest jakis szczegolny przypadek (brak jobow do wykonania, bo po prostu user mogl nie zlecic zadan)
            /*if (deviceJobs == null)
            {
                return NotFound($"There is no device job in database.");
            }*/


            var deviceJobsDTO = _mapper.Map<List<ViewDeviceJobDTO>>(deviceJobs);

            //OLD
            /*List<ViewDeviceJobDTO> deviceJobsDTO = new List<ViewDeviceJobDTO>();
            foreach (DeviceJob d in deviceJobs)
            {
                ViewDeviceDTO deviceDTO = _mapper.Map<ViewDeviceDTO>(d.Device);

                JobDTO jobDTO = _mapper.Map<JobDTO>(d.Job);

                ViewDeviceJobDTO deviceJobDTO = _mapper.Map<ViewDeviceJobDTO>(d);
                deviceJobDTO.Job = jobDTO;

                deviceJobsDTO.Add(deviceJobDTO);
            }*/

            return Ok(deviceJobsDTO);
        }

        // GET: api/device-jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewDeviceJobDTO>> GetDeviceJob(int id)
        {
            var deviceJob = await _context.DeviceJobs.Include(d => d.Device).Include(j => j.Job).Where(deviceJobs => deviceJobs.Id == id).Include(d => d.Device.DeviceType).Include(d => d.Job.DeviceType).FirstOrDefaultAsync();
            
            if (deviceJob == null)
            {
                return NotFound($"There is no device job for given id: {id}.");
            }

            ViewDeviceJobDTO deviceJobDTO = _mapper.Map<ViewDeviceJobDTO>(deviceJob);
            
            return Ok(deviceJobDTO);
        }

        // GET: api/device-jobs/device/{deviceId}
        [HttpGet("device/{deviceId}")]
        public async Task<ActionResult<IEnumerable<ViewDeviceJobDTO>>> GetDeviceJobForDevice(int deviceId)
        {
            var deviceExist = await _context.Devices.AnyAsync(device => device.Id == deviceId);

            if (deviceExist == false)
            {
                return BadRequest($"There is no device for given id: {deviceId}.");
            }

            List<DeviceJob> deviceJobs = await _context.DeviceJobs
                .Where(d => d.Device.Id == deviceId)
                .Include(j => j.Job)
                .ThenInclude(j => j.DeviceType)
                .Include(d => d.Device.DeviceType).ToListAsync();

            var deviceJobsDTO = _mapper.Map<List<ViewDeviceJobDTO>>(deviceJobs);
            
            return Ok(deviceJobsDTO);
        }

        // GET: api/device-jobs/values/5
        [HttpGet("values/{valueId}")]
        public async Task<ActionResult<ViewDeviceJobValueDTO>> GetDeviceJobValue(int valueId)
        {
            var value = await _context.Values.Include(v => v.Device).Include(v => v.Property).Include(v => v.DeviceJob).Where(v => v.Id == valueId).FirstOrDefaultAsync();
            if (value == null)
            {
                return NotFound($"There is no value for given id: {valueId}.");
            }

            if (value.DeviceJob == null)
            {
                return NotFound($"There is no value with given id: {valueId} for any device job.");
            }

            ViewDeviceJobValueDTO viewValueDTO = _mapper.Map<ViewDeviceJobValueDTO>(value);
            return Ok(viewValueDTO);
        }

        // GET: api/get-last-status-changes/100
        [HttpGet("get-last-status-changes/{amount}")]
        public async Task<ActionResult<IEnumerable<ViewDeviceJobDTO>>> GetLatestDeviceStatusHistory(int amount)
        {
            if (amount > 10000)
            {
                amount = 10000;
            }

            IEnumerable<DeviceJob> deviceJobsStatus = await _context.DeviceJobs
                .OrderByDescending(d => d.StatusChanged).Take(amount).ToListAsync();

            //DB jesli takelast jest po toListAsync to: z bazy pobiora sie wszystkie wartosci, a dopiero pozniej z nich zwrocisz amount, i tak jest zle
            //to baza ma zwrocic ograniczona ilosc rekordow a wiec: takelast PRZED to list

            if (deviceJobsStatus == null)
            {
                return NotFound($"There is no device jobs.");
            }

            return _mapper.Map<List<ViewDeviceJobDTO>>(deviceJobsStatus);
        }

        // GET: api/device-jobs/5/get-all-values
        [HttpGet("{id}/get-all-job-values")]
        public async Task<ActionResult<IEnumerable<ViewDeviceJobValueDTO>>> GetAllDeviceJobValues(int id)
        {
            var values = await _context.Values.Include(v => v.Device).Include(v => v.Property).Include(v => v.DeviceJob).Where(v => v.DeviceJobId == id).ToListAsync();

            if (values == null)
            {
                return NotFound($"There is no values for device job with given id: {id}.");
            }

            List<ViewDeviceJobValueDTO> viewValuesDTO = _mapper.Map<List<ViewDeviceJobValueDTO>>(values);
            return Ok(viewValuesDTO);
        }

        // GET: api/device-jobs/5/export-all-job-values
        [HttpGet("{id}/export-all-job-values")]
        public IActionResult ExportLastDeviceJobValues(int id)
        {
            var values = _context.Values.Include(v => v.Device).Include(v => v.Property).Include(v => v.DeviceJob).Where(v => v.DeviceJobId == id).ToList();

            if (values == null)
            {
                return NotFound($"There is no values for device job with given id:: {id}.");
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Device_Job_Values");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Value";
                worksheet.Cell(currentRow, 2).Value = "DateTime";
                worksheet.Cell(currentRow, 3).Value = "Device Name";
                worksheet.Cell(currentRow, 4).Value = "Device ID";
                worksheet.Cell(currentRow, 5).Value = "Property Name";
                worksheet.Cell(currentRow, 6).Value = "Property ID";
                worksheet.Cell(currentRow, 7).Value = "Job Name";
                worksheet.Cell(currentRow, 8).Value = "DeviceJob ID";

                foreach (var v in values)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = v.Val;
                    worksheet.Cell(currentRow, 2).Value = v.DateTime;
                    worksheet.Cell(currentRow, 3).Value = v.Device.Name;
                    worksheet.Cell(currentRow, 4).Value = v.DeviceId;
                    worksheet.Cell(currentRow, 5).Value = v.Property.Name;
                    worksheet.Cell(currentRow, 6).Value = v.PropertyId;
                    worksheet.Cell(currentRow, 7).Value = v.DeviceJob.Job.Name;
                    worksheet.Cell(currentRow, 8).Value = v.DeviceJobId;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "dev_job_values.xlsx");
                }
            }
        }

        // GET: api/device-jobs/device/{id}/flase-done-flag
        [HttpGet("device/{deviceId}/false-done-flag")]
        public async Task<ActionResult<ViewDeviceJobDTO>> GetDeviceJobFalseDoneFlag(int deviceId)
        {
            var device = await _context.Devices.Where(device => device.Id == deviceId).FirstOrDefaultAsync();

            if (device == null)
            {
                return BadRequest($"There is no device for given id: {deviceId}.");
            }

            var deviceJob = await _context.DeviceJobs
                .Where(d => d.Status == DeviceJobStatus.Created && d.Device.Id == deviceId)
                .OrderBy(d => d.CreatedDate)
                .Include(d => d.Job)
                    .ThenInclude(j => j.DeviceType)
                .FirstOrDefaultAsync();
            
            ViewDeviceJobDTO deviceJobDTO = _mapper.Map<ViewDeviceJobDTO>(deviceJob);
            
            if (deviceJob != null)
            {
                deviceJob.Status = DeviceJobStatus.Submitted;
                deviceJob.StatusChanged = DateTime.Now;
                _context.Entry(deviceJob).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return Ok(deviceJobDTO);
        }

        // GET: api/device-jobs/device/{id}/all-flase-done-flag
        [HttpGet("device/{deviceId}/all-flase-done-flag")]
        public async Task<ActionResult<IEnumerable<ViewDeviceJobDTO>>> GetAllDeviceJobsFalseDoneFlag(int deviceId)
        {
            var device = await _context.Devices.Where(device => device.Id == deviceId).FirstOrDefaultAsync();

            if (device == null)
            {
                return BadRequest($"There is no device for given id: {deviceId}.");
            }

            var deviceJobs = await _context.DeviceJobs
                .Where(deviceJob => deviceJob.Status == DeviceJobStatus.Created && deviceJob.Device.Id == deviceId)
                .OrderBy(d => d.CreatedDate)
                .Include(d => d.Job)
                    .ThenInclude(j => j.DeviceType)
                .ToListAsync();
            

            var deviceJobsDTO = _mapper.Map<List<ViewDeviceJobDTO>>(deviceJobs);

            if (deviceJobs.Count != 0)
            {
                deviceJobs.ForEach(el => { el.Status = DeviceJobStatus.Submitted; el.StatusChanged = DateTime.Now; });
                deviceJobs.ForEach(el => _context.Entry(el).State = EntityState.Modified);
                await _context.SaveChangesAsync();
            }
            
            return Ok(deviceJobsDTO);
        }

        // PUT: api/device-jobs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceJob(int id, AddDeviceJobDTO deviceJob)
        {
            /*if (id != deviceJob.Id)
            {
                return BadRequest();
            }*/

            var deviceJobToUpdate = await _context.DeviceJobs.Where(d => d.Id == id).Include(d => d.Device).FirstOrDefaultAsync();

            if (deviceJobToUpdate == null)
            {
                return NotFound($"There is no device job for given id: {id}.");
            }

            deviceJobToUpdate.ExecutionTime = deviceJob.ExecutionTime;
            deviceJobToUpdate.Body = deviceJob.Body;

            _context.Entry(deviceJobToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceJobExists(id))
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

        // PATCH: api/device-jobs/5/done-flag-value
        [HttpPatch("{id}/done-flag-value")]
        public async Task<IActionResult> UpdateDoneProperty(int id, IsDoneFlagDTO DoneFlag)
        {
            var deviceJobToUpdate = await _context.DeviceJobs
                .Where(d => d.Id == id)
                .Include(d => d.Job)
                .ThenInclude(j => j.DeviceType)
                .FirstOrDefaultAsync();

            if (deviceJobToUpdate == null)
            {
                return NotFound($"There is no deviceJob for given id: {id}.");
            }

            deviceJobToUpdate.Done = DoneFlag.Done;
            if (DoneFlag.Done == true)
            {
                deviceJobToUpdate.Status = DeviceJobStatus.Completed;
                deviceJobToUpdate.StatusChanged = DateTime.Now;
            }

            _context.Entry(deviceJobToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceJobExists(id))
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

        // POST: api/device-jobs/device/{deviceId}/job/{jobId}
        [HttpPost("device/{deviceId}/job/{jobId}")]
        public async Task<ActionResult<ViewDeviceJobDTO>> PostDeviceJob(int deviceId, int jobId, AddDeviceJobDTO deviceJob)
        {
            var device = await _context.Devices.Where(d => d.Id == deviceId).Include(d => d.DeviceType).FirstOrDefaultAsync();

            if (device == null)
            {
                return NotFound($"There is no device for given id: {deviceId}.");
            }

            var job = await _context.Jobs.Include(j => j.DeviceType).Where(j => j.Id == jobId).FirstOrDefaultAsync();
            if (job == null)
            {
                return NotFound($"There is no job for given id: {jobId}.");
            }

            if (device.DeviceType.Id != job.DeviceType.Id)
            {
                return BadRequest("Requested job cannot be performed by given device. Device types do not match.");
            }

            ViewDeviceDTO deviceDTO = _mapper.Map<ViewDeviceDTO>(device);

            JobDTO jobDTO = _mapper.Map<JobDTO>(job);

            var newDeviceJob = new DeviceJob()
            {
                StatusChanged = DateTime.Now,
                CreatedDate = DateTime.Now,
                ExecutionTime = deviceJob.ExecutionTime,
                Body = deviceJob.Body,
                Device = device,
                Job = job
            };

            _context.Entry(device).State = EntityState.Modified;
            _context.Entry(job).State = EntityState.Modified;

            await _context.DeviceJobs.AddAsync(newDeviceJob);
            await _context.SaveChangesAsync();

            ViewDeviceJobDTO deviceJobDTO = _mapper.Map<ViewDeviceJobDTO>(newDeviceJob);
            
            deviceJobDTO.Job = jobDTO;

            return CreatedAtAction("GetDeviceJob", new { id = deviceJobDTO.Id }, deviceJobDTO);
        }

        // DELETE: api/device-jobs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ViewDeviceJobDTO>> DeleteDeviceJob(int id)
        {
            var deviceJob = await _context.DeviceJobs.FindAsync(id);
            if (deviceJob == null)
            {
                return NotFound($"There is no device job for given id: {id}.");
            }

            _context.DeviceJobs.Remove(deviceJob);
            await _context.SaveChangesAsync();

            ViewDeviceJobDTO deviceJobToView = _mapper.Map<ViewDeviceJobDTO>(deviceJob);
            return Ok(deviceJobToView);
        }

        private bool DeviceJobExists(int id)
        {
            return _context.DeviceJobs.Any(e => e.Id == id);
        }
    }
}
