using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(1)]
        public string Name { get; set; }
        [Required, MinLength(1)]
        public string Ticker { get; set; }
        public List<Quotation> Quotations { get; set; }
    }
}
