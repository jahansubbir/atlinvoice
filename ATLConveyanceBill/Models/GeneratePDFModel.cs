using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATLConveyanceBill.Models
{
    public class GeneratePDFModel
    {
        public Employee Employee { get; set; }
        public ConveyanceViewModel ViewModel { get; set; }
        public decimal TotalAmount { get; set; }

    }
}