using BackendTestTask.Entities;
using BackendTestTask.Helpers;
using BackendTestTask.Models.Requests;
using BackendTestTask.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        //[Authorize(Roles = Role.Admin)]
        [Authorize]
        [HttpGet("get")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
            //return Ok(int.Parse(this.User.Claims.First().Value));
        }
    }
}
