using BackendTestTask.Entities;
using BackendTestTask.Helpers;
using BackendTestTask.Models.Requests;
using BackendTestTask.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Controllers
{
    /// <summary>
    /// Контроллер пользователей
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Аутентифицирует пользователя
        /// </summary>
        /// <param name="model">Модель запроса аутентификации</param>
        /// <returns>Данные пользователя, если запрос был выполнен корректно, или BadRequest, если не был выполнен</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            _logger.LogInformation("Starting authenticate request");
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
            {
                _logger.LogWarning("Failed to authenticate");
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            _logger.LogInformation("Returning a response to a request for authenticate");
            return Ok(user);
        }
    }
}
