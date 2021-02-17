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
        User Authenticate(string username, string password);
    }
}
