using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTestTask.Entities
{
    public class Quotation
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int SourceId { get; set; }
        public Source Source { get; set; }
        public double Price { get; set; }
        public string CurrencyUnit { get; set; }
        public DateTime Date { get; set; }
    }
}
