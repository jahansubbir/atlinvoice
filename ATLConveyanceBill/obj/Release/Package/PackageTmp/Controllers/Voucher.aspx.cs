using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace ATLConveyanceBill.Controllers
{
    public partial class Vouocher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var conveyanceList = Session["Query"];
            ReportDocument reportDocument = new ReportDocument();

            reportDocument.Load(Path.Combine(Server.MapPath("~/Reports"), "Voucher.rpt"));
            reportDocument.SetDatabaseLogon("", "", @"DESKTOP-1TH4N6D\SQLEXPRESS", "ConveyanceDB");

            reportDocument.SetDataSource(conveyanceList);
            NetVoucher.ReportSource = reportDocument;
            NetVoucher.RefreshReport();
        }
    }
}