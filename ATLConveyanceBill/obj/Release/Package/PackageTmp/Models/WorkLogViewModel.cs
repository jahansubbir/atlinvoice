using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ATLConveyanceBill.Models
{
    public class WorkLogViewModel
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }
        [DisplayName("Emp Id")]
        public string EmpId { get; set; }
        [DisplayName("Date")]
        public DateTime Date { get; set; }
        [DisplayName("Project")]
        public string Project { get; set; }
        [DisplayName("Work System")]
        public string WorkSystem { get; set; }
        [DisplayName("Work Type")]
        public string WorkType { get; set; }
        public string Description { get; set; }


    }
}