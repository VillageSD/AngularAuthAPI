using AngularAuthAPI.Context;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private readonly AuthDbContext authApiContext;
        public Users(AuthDbContext dbContext)
        {
            authApiContext = dbContext;
        }

        [HttpPost("Authorize")]
        public async Task<IActionResult> AuthenticateUser([FromBody]User user)
        {
            Console.WriteLine($"Authenticating User {user.FirstName}!");

            if (user == null) { return BadRequest(); }
            var x = await authApiContext
                        .Users
                        .FirstOrDefaultAsync<User>(u => u.UserName == user.UserName && u.Password == user.Password);
            if (x == null)
                return StatusCode(404, "User Not Found");
            else
                return Ok();
            
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            Console.WriteLine("Registering User!");

            if (user == null) { return BadRequest(); }
            await authApiContext.Users.AddAsync(user);
            await authApiContext.SaveChangesAsync();
            return Ok();
        }
    }
}
