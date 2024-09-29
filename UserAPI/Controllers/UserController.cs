using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly UserModelContext _context;

        public UserDataController(UserModelContext context)
        {
            _context = context;
        }

        // Get all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetusersModel()
        {
            try
            {
                var users = await _context.UserTable1.ToListAsync();
                if (users.Count > 0)
                {
                    return Ok(users);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while getting the users.");
            }
        }

        // Get one user by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetusersModel(int id)
        {
            try
            {
                var user = await _context.UserTable1.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(500, "Cannot get the requested user.");
            }
        }

        // Create user
        [HttpPost]
        public async Task<ActionResult<UserModel>> PostusersModel(UserModel user)
        {
            try
            {
                _context.UserTable1.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetusersModel), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(500, "Cannot execute the POST request.");
            }
        }

        // Update user
        [HttpPut("{id}")]
        public async Task<IActionResult> PutusersModel(int id, UserModel user)
        {
            // Fetch the user from the database
            var existingUser = await _context.UserTable1.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            // Ensure the ID matches
            if (id != user.Id)
            {
                return BadRequest("The user ID in the URL does not match the ID in the body.");
            }

            // Update the fetched user with the new data
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound("User does not exist.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Delete user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteusersModel(int id)
        {
            var user = await _context.UserTable1.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.UserTable1.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.UserTable1.Any(e => e.Id == id);
        }
    }
}
