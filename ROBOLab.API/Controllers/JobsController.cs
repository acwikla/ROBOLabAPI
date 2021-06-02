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

        // GET: api/jobs/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<JobDTO>>> GetJobs()
        {
            // lazy loading jest wlaczony wiec include zrobi sie sam podczas mapowania,
            // ale jeden include tutaj bedzie wydajniejszy
            var jobs = await _context.Jobs
                .Include(j=> j.DeviceType)    
                .ToListAsync();

            return _mapper.Map<List<JobDTO>>(jobs);
        }

        // GET: api/jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDTO>> GetJob([FromRoute] int id)
        {
            var job = await _context.Jobs
                .Include(j => j.DeviceType)
                .Where(j => j.Id == id)
                .FirstOrDefaultAsync();

            if (job == null)
            {
                return NotFound($"There is no job for given id: {id}.");
            }



            //IQueryable<Job> test = _context.Jobs
            //    .Include(j => j.DeviceType);
            //.Where(j => j.Id == id);


            //var test2 = test.Where(.....);



            //int a = test.First().DeviceTypeId;


            return _mapper.Map<JobDTO>(job);
        }

        // GET: api/jobs?devtype=devtype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDTO>>> GetJobForDeviceType([FromQuery] string devtype)
        {
            var deviceType = await _context.DeviceTypes
                .Include(d => d.Jobs)
                .Where(deviceType => deviceType.Name == devtype)
                .FirstOrDefaultAsync();

            if (deviceType == null)
            {
                return NotFound($"There is no device type with given name: {devtype}.");
            }

            if (deviceType.Jobs == null)
            {
                //return NotFound($"There is no job for device type with given name: {devtype}.");
                return new List<JobDTO>();  // moze nie byc jobow i nie jest to blad wiec lepiej zwrocic pusta liste
            }

            return _mapper.Map<List<JobDTO>>(deviceType.Jobs);
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
            jobToUpdate.Properties = jobDTO.Properties;

            // TODO: jesli nie aktualizujemy device type (co jest ok) to obiekt jobDTO
            // nie powinien posidac pola DeviceType (mozna zrobic inny dto np: JobUpdate)

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
        public async Task<ActionResult<AddJobDTO>> PostJob(AddJobDTO jobDTO)
        {
            // get dev type
            var deviceType = await _context.DeviceTypes
                .Where(dt => dt.Name == jobDTO.DeviceTypeName)
                .FirstOrDefaultAsync();

            if (deviceType == null)
            {
                return BadRequest($"There is no device type: {jobDTO.DeviceTypeName}.");
            }

            // set dev type
            var job = _mapper.Map<Job>(jobDTO);
            job.DeviceType = deviceType;
            _context.Jobs.Add(job);// brakowalo tej linii
            await _context.SaveChangesAsync();

            var jobToViewDTO = _mapper.Map<AddJobDTO>(job);
            return CreatedAtAction(nameof(GetJob), new { id = job.Id }, jobDTO);
        }

        // DELETE: api/jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound($"There is no job for given id: {id}.");
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
