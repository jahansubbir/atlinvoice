using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATLConveyanceBill.Models
{
    public class Approval
    {
        public List<int> Id { get; set; }
        public decimal Amount { get; set; }
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "From")]
        public DateTime From { get; set; }
        [Required(ErrorMessage = "To")]
        public DateTime To { get; set; }
        //public bool Check { get; set; }

        public bool Approve { get; set; }
        //public bool Pay { get; set; }
    }
}