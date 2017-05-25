using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ATLConveyanceBill.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DisplayName("Employee Id")]
        public string EmpId { get; set; }

        [Required]
        [DisplayName("Designation")]
        public int DesignationId { get; set; }
        [Required]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [Required]
        //[NotMapped]
        //[PasswordPropertyText]
        public string Password { get; set; }
        //[NotMapped]
        [Required]
        //[PasswordPropertyText]
        [Compare("Password",ErrorMessage = "Password doesn't match")]
        public string ConfirmPassword { get; set; }

        public int Status { get; set; }

    }
}