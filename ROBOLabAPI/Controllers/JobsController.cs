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
    [Route("api/jobs")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly ROBOLabDbContext _context;
        private readonly IMapper _mapper;

        public JobsController(ROBOLabDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDTO>>> GetJobs()
        {
            var jobs = await _context.Jobs.ToListAsync();

            List<JobDTO> jobsDTO = new List<JobDTO>();
            foreach (Job j in jobs)
            {
                JobDTO jobDTO = _mapper.Map<JobDTO>(j);
                jobsDTO.Add(jobDTO);
            }

            return jobsDTO;
        }

        // GET: api/jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDTO>> GetJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound($"There is no job for given id: {id}.");
            }

            JobDTO jobDTO = _mapper.Map<JobDTO>(job);
            return jobDTO;
        }

        // GET: api/jobs?devtype={type}
        [HttpGet("{devtype}")]
        public async Task<ActionResult<IEnumerable<JobDTO>>> GetJobForDeviceType([FromQuery] string type)
        {
            var deviceType = await _context.DeviceTypes.Include(d => d.Jobs).Where(deviceType => deviceType.Name == type).FirstOrDefaultAsync();

            if (deviceType == null)
            {
                return NotFound($"There is no device type with given name: {type}.");
            }

            if (deviceType.Jobs == null)
            {
                return NotFound($"There is no job for device type with given name: {type}.");
            }

            List<JobDTO> jobsDTO = new List<JobDTO>();
            foreach (Job j in deviceType.Jobs)
            {
                JobDTO jobDTO = _mapper.Map<JobDTO>(j);
                jobsDTO.Add(jobDTO);
            }
            
            return jobsDTO;
        }

        // PUT: api/jobs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, JobDTO jobDTO)
        {
            /*if (id != job.Id)
            {
                return BadRequest();
            }*/

            var jobToUpdate = await _context.Jobs.FindAsync(id);

            if (jobToUpdate == null)
            {
                return NotFound($"There is no job for given id: {id}.");
            }

            jobToUpdate.Name = jobDTO.Name;
            jobToUpdate.Description = jobDTO.Description;
            jobToUpdate.JobProperties = jobDTO.JobProperties;

            _context.Entry(jobToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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

        // POST: api/jobs
        [HttpPost]
        public async Task<ActionResult<JobDTO>> PostJob(JobDTO jobDTO)
        {
            var deviceType = await _context.DeviceTypes.Include(d => d.Jobs).Where(deviceType => deviceType.Name == jobDTO.DeviceType.DeviceTypeName).FirstOrDefaultAsync();

            Job job = _mapper.Map<Job>(jobDTO);

            _context.Jobs.Add(job);
            deviceType.Jobs.Add(job);
            await _context.SaveChangesAsync();

            JobDTO jobToViewDTO = _mapper.Map<JobDTO>(job);
            return CreatedAtAction("GetJob", new { id = jobToViewDTO.Id }, jobToViewDTO);
        }

        // DELETE: api/jobs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<JobDTO>> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            JobDTO jobDTO = _mapper.Map<JobDTO>(job);
            return jobDTO;
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
