using BackendTestTask.Entities;
using BackendTestTask.Helpers;
using BackendTestTask.Models.Requests;
using BackendTestTask.Models.Responses;
using BackendTestTask.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            _logger.LogInformation("Starting authenicate response");

            if (model.Password.Length == 0 || model.Username.Length == 0)
            {
                _logger.LogWarning("Invalid input data");
                return BadRequest("Please fill in the login and password fields");
            }

            var user = _userService.GetUserByUsername(model.Username);

            if (user == null || user.Salt == null)
            {
                _logger.LogWarning("User has not been found in database");
                return NotFound("User with specified username and password has not been found");
            }

            var verificationPassword = user.Password + Convert.ToBase64String(user.Salt) + model.Salt;

            if (BCrypt.Net.BCrypt.Verify(verificationPassword, model.Password))
            {
                _logger.LogInformation("Authenticate has been done");
                user = _userService.Authenticate(user);
                return Ok(user);
            }

            _logger.LogInformation("Use has not been found");
            return NotFound("User with specified username and password has not been found");

        }

        [AllowAnonymous]
        [HttpPost("get-salt")]
        public IActionResult GetSalt([FromBody] GetSaltRequest model)
        {
            _logger.LogInformation("Starting get-salt request");
            if (model.Username.Length == 0)
            {
                _logger.LogWarning("Invalid unput data");
                return BadRequest("Please fill in the login and password fields");
            }

            var salt = generateRandomBytes();

            _userService.SetUserSalt(model.Username, salt);
            _logger.LogInformation("Return salt");
            return File(salt, "application/octet-stream");
        }

        private byte[] generateRandomBytes()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(salt);
            }
            return salt;
        }
    }
}
