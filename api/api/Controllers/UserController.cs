using System.Linq;
using api.Helpers;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly ILogger _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [Authorize(Roles = "none")]
        [HttpGet]
        public IActionResult GetAll()
        {
            
            var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "userid").SingleOrDefault();
            User _user = Userclaim.getUserById(userIdClaim);
            _logger.LogInformation("Logged in user : " + _user.Name);
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}