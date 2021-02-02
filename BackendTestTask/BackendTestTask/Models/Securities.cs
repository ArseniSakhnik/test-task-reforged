using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Models
{
    public class Securities
    {
        public List<string> columns { get; set; }
        public List<List<string>> data { get; set; }
    }
}
