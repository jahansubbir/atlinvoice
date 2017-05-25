using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATLConveyanceBill.Models
{
    public class Conveyance
    {
        public int Id { get; set; }
        
        [Required]
        [DisplayName("Employee")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Travel Date")]
        
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "From ")]
        public string From { get; set; }
        [Required(ErrorMessage = "To")]
        public string To { get; set; }
        [Required]
        [DisplayName("Vehicle By")]
        public string VehicleBy { get; set; }
        [Required]
        [DisplayName("Project")]
        public int ProjectId { get; set; }
        [Required(ErrorMessage = @"Please Give The Amount")]
        public int Amount { get; set; }
        public string Remarks { get; set; }
        public bool Checked { get; set; }
        public bool Approved { get; set; }
        public bool Paid { get; set; }
        public string WorkLogNo { get; set; }

    }
}