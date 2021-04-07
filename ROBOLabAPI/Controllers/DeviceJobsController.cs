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
        public async Task<ActionResult<IEnumerable<DeviceJob>>> GetDeviceJobs()
        {
            return await _context.DeviceJobs.ToListAsync();
        }

        // GET: api/device-jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceJobToViewDTO>> GetDeviceJob(int id)
        {
            var deviceJob = await _context.DeviceJobs.Include(d => d.Device).Include(j => j.Job).Where(deviceJobs => deviceJobs.Id == id).FirstOrDefaultAsync();

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

            DeviceToViewDTO deviceDTO = _mapper.Map<DeviceToViewDTO>(deviceJob.Device);

            JobDTO jobDTO = _mapper.Map<JobDTO>(deviceJob.Job);

            DeviceJobToViewDTO deviceJobDTO = _mapper.Map<DeviceJobToViewDTO>(deviceJob);
            deviceJobDTO.Device = deviceDTO;
            deviceJobDTO.Job = jobDTO;

            return Ok(deviceJobDTO);
        }

        // GET: api/device-jobs/device/{deviceId}
        [HttpGet("/device/{deviceId}")]
        public async Task<ActionResult<IEnumerable<DeviceJobToViewDTO>>> GetDeviceJobForDevice(int deviceId)
        {
            List<DeviceJob> deviceJobs = await _context.DeviceJobs.Include(d => d.Device).Include(j => j.Job).Where(deviceJob => deviceJob.Device.Id == deviceId).ToListAsync();
            
            if (deviceJobs == null)
            {
                return NotFound($"There is no device job for device with given id: {deviceId}.");
            }

            List<DeviceJobToViewDTO> deviceJobsDTO = new List<DeviceJobToViewDTO>();

            foreach (DeviceJob d in deviceJobs)
            {
                DeviceToViewDTO deviceDTO = _mapper.Map<DeviceToViewDTO>(d.Device);

                JobDTO jobDTO = _mapper.Map<JobDTO>(d.Job);

                DeviceJobToViewDTO deviceJobDTO = _mapper.Map<DeviceJobToViewDTO>(d);
                deviceJobDTO.Device = deviceDTO;
                deviceJobDTO.Job = jobDTO;

                deviceJobsDTO.Add(deviceJobDTO);
            }

            return deviceJobsDTO;
        }

        // GET: api/device-jobs/device/{id}/flase-done-flag
        [HttpGet("device/{deviceId}/flase-done-flag")]
        public async Task<ActionResult<DeviceJobToViewDTO>> GetDeviceJobFalseDoneFlag(int deviceId)
        {
            List<DeviceJob> deviceJobs = await _context.DeviceJobs.Where(deviceJob => deviceJob.Done == false).Include(d => d.Device).Include(j => j.Job).Where(deviceJob => deviceJob.Device.Id == deviceId).ToListAsync();

            var deviceJob = deviceJobs.FirstOrDefault();
            if (deviceJob == null)
            {
                return NotFound($"There is no device job with false isDone flag for device with given id: {deviceId}.");
            }

            DeviceToViewDTO deviceDTO = _mapper.Map<DeviceToViewDTO>(deviceJob.Device);

            JobDTO jobDTO = _mapper.Map<JobDTO>(deviceJob.Job);

            DeviceJobToViewDTO deviceJobDTO = _mapper.Map<DeviceJobToViewDTO>(deviceJob);
            deviceJobDTO.Device = deviceDTO;
            deviceJobDTO.Job = jobDTO;

            //deviceJobs.Remove(deviceJob);
            return Ok(deviceJobDTO);
        }

        // PUT: api/device-jobs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceJob(int id, DeviceJobAddDTO deviceJob)
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

            var deviceToUpdate = deviceJobToUpdate.Device;

            _context.Entry(deviceToUpdate).State = EntityState.Modified;
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

        // PATCH: api/device-jobs/5
        [HttpPatch("{id}")]
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
        [HttpPost("device-jobs/device/{deviceId}/job/{jobId}")]
        public async Task<ActionResult<DeviceJobToViewDTO>> PostDeviceJob(int deviceId, int jobId, DeviceJobAddDTO deviceJob)
        {
            var device = await _context.Devices.FindAsync(deviceId);

            if (device == null)
            {
                return NotFound($"There is no device for given id: {deviceId}.");
            }

            var job = await _context.Jobs.FindAsync(jobId);
            if (job == null)
            {
                return NotFound($"There is no job for given id: {jobId}.");
            }

            DeviceToViewDTO deviceDTO = _mapper.Map<DeviceToViewDTO>(device);

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

            DeviceJobToViewDTO deviceJobDTO = _mapper.Map<DeviceJobToViewDTO>(deviceJob);
            deviceJobDTO.Device = deviceDTO;
            deviceJobDTO.Job = jobDTO;

            return CreatedAtAction("GetDeviceJob", new { id = deviceJobDTO.Id }, deviceJobDTO);
        }

        // DELETE: api/device-jobs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeviceJobToViewDTO>> DeleteDeviceJob(int id)
        {
            var deviceJob = await _context.DeviceJobs.FindAsync(id);
            if (deviceJob == null)
            {
                return NotFound($"There is no device job for given id: {id}.");
            }

            _context.DeviceJobs.Remove(deviceJob);
            await _context.SaveChangesAsync();

            DeviceJobToViewDTO deviceJobToView = _mapper.Map<DeviceJobToViewDTO>(deviceJob);
            return Ok(deviceJobToView);
        }

        private bool DeviceJobExists(int id)
        {
            return _context.DeviceJobs.Any(e => e.Id == id);
        }
    }
}
