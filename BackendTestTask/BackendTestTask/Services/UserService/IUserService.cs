using BackendTestTask.Entities;
using BackendTestTask.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Services.UserService
{
    /// <summary>
    /// Сервис для добавления сервиса
    /// </summary>
    public interface IUserService
    {
        User Authenticate(User user);
        bool SetUserSalt(string username, byte[] salt);
        bool RemoveUserSalt(string username);
        User GetUserByUsername(string username);
    }
}
