using BackendTestTask.Entities;
using BackendTestTask.Helpers;
using BackendTestTask.Models.Requests;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.UserService
{
    /// <summary>
    /// Сервис для работы с данными пользователями
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly DataContext _dataContext;
        private readonly ILogger<UserService> _logger;

        public UserService(IOptions<AppSettings> appSettings, DataContext dataContext, ILogger<UserService> logger)
        {
            _appSettings = appSettings;
            _dataContext = dataContext;
            _logger = logger;
        }
        /// <summary>
        /// Аутентифицирует пользовяет
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns></returns>
        public User Authenticate(User user)
        {
            // return null if user not found
            if (user == null)
            {
                return null;
            }

            _logger.LogInformation("Creating user token");
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            _logger.LogInformation("User without password and salt has been returned");

            return user.WithoutPassword().WithoutSalt();
        }

        public User GetUserByUsername(string username)
        {
            _logger.LogInformation("Set user salt in database");
            var user = _dataContext.Users.SingleOrDefault(u => u.Username == username);

            return user;
        }

        public bool RemoveUserSalt(string username)
        {
            _logger.LogInformation("Remove user salt in database");
            try
            {
                var user = _dataContext.Users.Where(u => u.Username == username).SingleOrDefault();

                if (user == null)
                {
                    _logger.LogInformation("User has not been found");
                    return false;
                }

                user.Salt = null;
                _dataContext.SaveChanges();
                _logger.LogInformation("Salt has been removed");
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogWarning($"An exception was received while working with the database: {ex.Message}");
                return false;
            }
        }


        public bool SetUserSalt(string username, byte[] salt)
        {
            _logger.LogInformation("Set user salt in database");
            try
            {
                var user = _dataContext.Users.Where(u => u.Username == username).SingleOrDefault();

                if (user == null)
                {
                    _logger.LogWarning("Use has not been found");
                    return false;
                }

                user.Salt = salt;
                _dataContext.SaveChanges();

                _logger.LogWarning("User salt has been removed");
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogWarning($"An exception was received while working with the database: {ex.Message}");
                return false;
            }
        }
    }
}
