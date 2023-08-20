using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Services;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)

        {
            _userService = userService;
        }

        // POST: api/Authenticate
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var token = _userService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }


        // POST: api/CreateNewUser
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> CreateNewUser([FromBody]RegisterUserModel model)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var addUserResult = await _userService.CreateNewUserAsync(model);

            if (!addUserResult.Success)
                return BadRequest(addUserResult.ErrorMessage);

            return Ok(addUserResult.User);
        }
    }
}
