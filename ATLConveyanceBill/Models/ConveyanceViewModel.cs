using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATLConveyanceBill.Models
{
    public class ConveyanceViewModel
    {

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public string Name { get; set; }
        public string EmpId { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Project { get; set; }
        public string VehicleBy { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public string Checked { get; set; }
        public string Approved { get; set; }
        public string Paid { get; set; }

    }
}