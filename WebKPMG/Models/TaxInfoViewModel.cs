using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebKPMG.Models
{
    public class TaxInfoViewModel
    {

        public int TaxId { get; set; }
        public string Account { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public Decimal Amount { get; set; }
    }
}