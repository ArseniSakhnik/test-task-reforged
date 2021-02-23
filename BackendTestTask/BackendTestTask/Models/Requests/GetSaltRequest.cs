using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Models.Responses
{
    public class GetSaltRequest
    {
        [Required]
        public string Username { get; set; }
    }
}
