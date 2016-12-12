using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace KPMGTest.Model
{
    public class Tax_Information
    {
        [Key]
        public Int64 TaxId { get; set; }
        public string Account { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public Decimal Amount { get; set; }
    }
}
