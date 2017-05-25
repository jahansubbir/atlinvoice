using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATLConveyanceBill.Models
{
    public class Login
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("User Name")]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }

        public DateTime Date { get; set; }

    }
}