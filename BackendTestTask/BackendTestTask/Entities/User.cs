using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendTestTask.Entities
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(1), MaxLength(50)]
        public string Username { get; set; }
        [Required, MinLength(1)]
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
