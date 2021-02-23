using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Models.Requests
{
    public class GetSaltResponse
    {
        [Required]
        public byte[] Salt { get; set; }
    }
}
