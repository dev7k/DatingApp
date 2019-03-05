using System.Threading.Tasks;
using DatingAppApi.Data;
using DatingAppApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingAppApi.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            // validate request

            username = username.ToLower();

            if(await _authRepository.UserExists(username))
            {
                return BadRequest("Username already exists");
            }

            var userToCreate = new User {Username = username};

            var createdUser = await _authRepository.Register(userToCreate, password);

            return StatusCode(201);
        }
    }
}
