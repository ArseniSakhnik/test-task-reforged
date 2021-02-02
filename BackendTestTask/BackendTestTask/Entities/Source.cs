using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Entities
{
    public class Source
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(1)]
        public string Name { get; set; }
        [Required, MinLength(1)]
        public string BaseAPIUrl { get; set; }
        public List<Quotation> Quotations { get; set; }
    }
}
