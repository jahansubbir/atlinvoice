using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATLConveyanceBill.Models
{
    public class Authenticate
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Starting Date")]
        public DateTime From { get; set; }
        [Required(ErrorMessage = "Ending Date")]
        public DateTime To { get; set; }
        public bool Check { get; set; }

        public bool Approve { get; set; }
        public bool Pay { get; set; }
    }
}