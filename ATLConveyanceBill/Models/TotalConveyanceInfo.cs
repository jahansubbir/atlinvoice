using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATLConveyanceBill.Models
{
    public class TotalConveyanceInfo
    {
        public WorkLog WorkLog { get; set; }
        public Conveyance Conveyance { get; set; }
    }
}