using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    public class UsersController : ControllerBase
    {
        private readonly ROBOLabDbContext _context;
        private readonly IMapper _mapper;
        public UsersController(ROBOLabDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserToViewDTO>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            if (users == null)
            {
                return NotFound($"There is no users in database.");
            }

            List<UserToViewDTO> usersViewDTO = new List<UserToViewDTO>();
            foreach (User u in users)
            {
                UserToViewDTO userViewDTO = _mapper.Map<UserToViewDTO>(u);
                usersViewDTO.Add(userViewDTO);
            }

            return Ok(usersViewDTO);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserToViewDTO>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound($"There is no user for given id: {id}.");
            }

            UserToViewDTO userViewDTO = _mapper.Map<UserToViewDTO>(user);
            return Ok(userViewDTO);
        }

        // GET: api/Users/5/device
        [HttpGet("{id}/device")]
        public async Task<ActionResult<IEnumerable<DeviceToViewDTO>>> GetUsersDevices(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound($"There is no user for given id: {id}.");
            }

            var userDevices = await _context.Users.Include(n => n.Devices).Where(user => user.Id == id).SelectMany(user => user.Devices).ToListAsync();

            if (userDevices == null)
            {
                return NotFound($"There is no devices for user with given id: {id}.");
            }

            List<DeviceToViewDTO> devicesToViewDTO = new List<DeviceToViewDTO>();
            foreach (Device d in userDevices)
            {
                DeviceToViewDTO deviceToViewDTO = _mapper.Map<DeviceToViewDTO>(d);
                devicesToViewDTO.Add(deviceToViewDTO);
            }

            return Ok(devicesToViewDTO);
        }

        // GET: api/Users/1/device/1
        [HttpGet("{id}/device/{deviceId}")]
        public async Task<ActionResult<DeviceToViewDTO>> GetUsersDevice(int id, int deviceId)
        {
            var user = await _context.Users.FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound($"There is no user for given id: {id}.");
            }

            var userDevices = await _context.Users.Include(n => n.Devices).Where(user => user.Id == id).SelectMany(user => user.Devices).ToListAsync();
            Device device = userDevices.Where(device => device.Id == deviceId).FirstOrDefault();

            if (device == null)
            {
                return NotFound($"There is no device for given id: {deviceId}.");
            }

            DeviceToViewDTO deviceToViewDTO = _mapper.Map<DeviceToViewDTO>(device);
            return Ok(deviceToViewDTO);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserRegisterDTO user)
        {
            /*if (id != UserRegisterDTO.Id)
            {
                return BadRequest();
            }*/

            var userToUpdate = await _context.Users.FindAsync(id);
            if (userToUpdate == null)
            {
                return NotFound($"There is no user for given id: {id}.");
            }

            userToUpdate.Password = HashPassword(user.Password);
            userToUpdate.Email = user.Email;
            userToUpdate.Login = user.Login;
            //userToUpdate = _mapper.Map<User>(user);

            _context.Entry(userToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserToViewDTO>> PostUser(UserRegisterDTO user)
        {
            if (EmailExists(user.Email))
            {
                return BadRequest($"User with this email already exists.");
            }

            user.Password = HashPassword(user.Password);
            User newUser = _mapper.Map<User>(user);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            UserToViewDTO userViewDTO = _mapper.Map<UserToViewDTO>(newUser);
            return CreatedAtAction("GetUser", new { id = userViewDTO.Id }, userViewDTO);
        }

        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserToLoginDTO userToLogin)
        {
            IActionResult response = Unauthorized();

            var user = await _context.Users.Where(user => user.Email == userToLogin.Email).FirstOrDefaultAsync();
            if (user != null)
            {
                bool confirmPassword = VerifyHashedPassword(user.Password, userToLogin.Password);

                if (confirmPassword)
                {
                    response = Ok("User has been successfully verified.");
                }
            }
            return response;
        }

        // POST: api/Users/1/device/device-type/{deviceTypeId}
        [HttpPost("{id}/device/deviceTypeId={deviceTypeId}")]
        public async Task<ActionResult<DeviceToViewDTO>> PostDevice(int id, int deviceTypeId, DeviceDTO device)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"There is no user for given id: {id}.");
            }

            var deviceType = await _context.DeviceTypes.FindAsync(deviceTypeId);
            if (deviceType == null)
            {
                return NotFound($"There is no device type for given id: {deviceTypeId}.");
            }

            Device newDevice = _mapper.Map<Device>(device);
            newDevice.UserId = user.Id;
            newDevice.User = user;
            newDevice.DeviceTypeId = deviceType.Id;
            newDevice.DeviceType = deviceType;

            await _context.Devices.AddAsync(newDevice);
            deviceType.Devices.Add(newDevice);
            await _context.SaveChangesAsync();

            DeviceToViewDTO deviceDTO = _mapper.Map<DeviceToViewDTO>(newDevice);
            return CreatedAtAction("GetUsersDevice", new { id, deviceId= deviceDTO.Id }, deviceDTO);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"There is no user for given id: {id}.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            UserToViewDTO userViewDTO = _mapper.Map<UserToViewDTO>(user);
            return Ok(userViewDTO);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private bool EmailExists(string email)
        {
            return _context.Users.Any(e => e.Email == email);
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }

            bool isEqual = buffer3.SequenceEqual(buffer4);
            return isEqual;
        }
    }
}
