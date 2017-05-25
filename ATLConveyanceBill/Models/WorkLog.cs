using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATLConveyanceBill.Models
{
    public class WorkLog
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Select Working Date")]

        public int EmployeeId { get; set; }
        public DateTime WorkDate { get; set; }

        [Required]
        [DisplayName("Company Name")]
        public int ProjectId { get; set; }
        [Required]
        [DisplayName("System Name")]
        public int WorkSystemId { get; set; }
        [Required]
        [DisplayName(" Work Type")]
        public int WorkTypeId { get; set; }
        [Required]
        [DisplayName("Work Description")]
        public string Description { get; set; }

        public string WorkLogNo { get; set; }

    }
}