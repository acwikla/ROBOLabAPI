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
            //without include device type in job and device
            //var deviceJob = await _context.DeviceJobs.Include(d => d.Device).Include(j => j.Job).Where(deviceJobs => deviceJobs.Id == id).FirstOrDefaultAsync();

            if (deviceJob == null)
            {
                return NotFound($"There is no device job for given id: {id}.");
            }

            if (deviceJob.Device == null)
            {
                return NotFound($"There is no device for device job with given id: {id}.");
            }

            if (deviceJob.Job == null)
            {
                return NotFound($"There is no job for device job with given id: {id}.");
            }

            ViewDeviceDTO deviceDTO = _mapper.Map<ViewDeviceDTO>(deviceJob.Device);

            JobDTO jobDTO = _mapper.Map<JobDTO>(deviceJob.Job);

            ViewDeviceJobDTO deviceJobDTO = _mapper.Map<ViewDeviceJobDTO>(deviceJob);
            
            deviceJobDTO.Job = jobDTO;

            return Ok(deviceJobDTO);
        }

        // GET: api/device-jobs/device/{deviceId}
        [HttpGet("/device/{deviceId}")]
        public async Task<ActionResult<IEnumerable<ViewDeviceJobDTO>>> GetDeviceJobForDevice(int deviceId)
        {
            List<DeviceJob> deviceJobs = await _context.DeviceJobs.Include(d => d.Device).Include(j => j.Job).Where(deviceJob => deviceJob.Device.Id == deviceId).Include(d => d.Device.DeviceType).Include(d => d.Job.DeviceType).ToListAsync();
            //without include device type in job and device
            //List<DeviceJob> deviceJobs = await _context.DeviceJobs.Include(d => d.Device).Include(j => j.Job).Where(deviceJob => deviceJob.Device.Id == deviceId).ToListAsync();

            if (deviceJobs == null)
            {
                return NotFound($"There is no device job for device with given id: {deviceId}.");
            }

            List<ViewDeviceJobDTO> deviceJobsDTO = new List<ViewDeviceJobDTO>();

            foreach (DeviceJob d in deviceJobs)
            {
                ViewDeviceDTO deviceDTO = _mapper.Map<ViewDeviceDTO>(d.Device);

                JobDTO jobDTO = _mapper.Map<JobDTO>(d.Job);

                ViewDeviceJobDTO deviceJobDTO = _mapper.Map<ViewDeviceJobDTO>(d);
                deviceJobDTO.Job = jobDTO;

                deviceJobsDTO.Add(deviceJobDTO);
            }

            return deviceJobsDTO;
        }

        //return first job with false value done flag
        // GET: api/device-jobs/device/{id}/flase-done-flag
        [HttpGet("device/{deviceId}/false-done-flag")]
        public async Task<ActionResult<ViewDeviceJobDTO>> GetDeviceJobFalseDoneFlag(int deviceId)
        {
            List<DeviceJob> deviceJobs = await _context.DeviceJobs.Where(deviceJob => deviceJob.Done == false).Include(d => d.Device).Include(j => j.Job).Where(deviceJob => deviceJob.Device.Id == deviceId).Include(d => d.Device.DeviceType).Include(d => d.Job.DeviceType).ToListAsync();
            //without include device type in job and device
            //List<DeviceJob> deviceJobs = await _context.DeviceJobs.Where(deviceJob => deviceJob.Done == false).Include(d => d.Device).Include(j => j.Job).Where(deviceJob => deviceJob.Device.Id == deviceId).ToListAsync();

            var deviceJob = deviceJobs.FirstOrDefault();
            if (deviceJob == null)
            {
                return NotFound($"There is no device job with false isDone flag for device with given id: {deviceId}.");
            }

            ViewDeviceDTO deviceDTO = _mapper.Map<ViewDeviceDTO>(deviceJob.Device);

            JobDTO jobDTO = _mapper.Map<JobDTO>(deviceJob.Job);

            ViewDeviceJobDTO deviceJobDTO = _mapper.Map<ViewDeviceJobDTO>(deviceJob);

            deviceJobDTO.Job = jobDTO;

            //deviceJobs.Remove(deviceJob);
            return Ok(deviceJobDTO);
        }

        //return all jobs with false value done flag
        // GET: api/device-jobs/device/{id}/all-flase-done-flag
        [HttpGet("device/{deviceId}/all-flase-done-flag")]
        public async Task<ActionResult<IEnumerable<ViewDeviceJobDTO>>> GetAllDeviceJobsFalseDoneFlag(int deviceId)
        {
            List<DeviceJob> deviceJobs = await _context.DeviceJobs.Where(deviceJob => deviceJob.Done == false).Include(d => d.Device).Include(j => j.Job).Where(deviceJob => deviceJob.Device.Id == deviceId).Include(d => d.Device.DeviceType).Include(d => d.Job.DeviceType).ToListAsync();
            //without include device type in job and device
            //List<DeviceJob> deviceJobs = await _context.DeviceJobs.Where(deviceJob => deviceJob.Done == false).Include(d => d.Device).Include(j => j.Job).Where(deviceJob => deviceJob.Device.Id == deviceId).ToListAsync();

            if (deviceJobs == null)
            {
                return NotFound($"There is no device job with false isDone flag for device with given id: {deviceId}.");
            }

            List<ViewDeviceJobDTO> deviceJobsToViewDTO = new List<ViewDeviceJobDTO>();
            foreach (DeviceJob d in deviceJobs)
            {
                ViewDeviceDTO deviceDTO = _mapper.Map<ViewDeviceDTO>(d.Device);

                JobDTO jobDTO = _mapper.Map<JobDTO>(d.Job);

                ViewDeviceJobDTO deviceJobDTO = _mapper.Map<ViewDeviceJobDTO>(d);
                
                deviceJobDTO.Job = jobDTO;

                deviceJobsToViewDTO.Add(deviceJobDTO);
            }
            
            //deviceJobs.Remove(deviceJob);
            return Ok(deviceJobsToViewDTO);
        }

        // PUT: api/device-jobs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceJob(int id, AddDeviceJobDTO deviceJob)
        {
            /*if (id != deviceJob.Id)
            {
                return BadRequest();
            }*/

            var deviceJobToUpdate = await _context.DeviceJobs.Include(d => d.Device).Where(deviceJobs => deviceJobs.Id == id).FirstOrDefaultAsync();

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
            var deviceJobToUpdate = await _context.DeviceJobs.Include(d => d.Device).Include(j => j.Job).Where(deviceJobs => deviceJobs.Id == id).FirstOrDefaultAsync();
            if (deviceJobToUpdate == null)
            {
                return NotFound($"There is no deviceJob for given id: {id}.");
            }

            deviceJobToUpdate.Done = DoneFlag.Done;

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
            //without include device type in job and device
            //TODO: check if the device type in device and job are the same
            var device = await _context.Devices.Include(d => d.DeviceType).Where(d => d.Id == deviceId).FirstOrDefaultAsync();

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
