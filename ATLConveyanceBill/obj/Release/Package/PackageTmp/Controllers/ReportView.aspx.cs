using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATLConveyanceBill.BLL;
using CrystalDecisions.CrystalReports.Engine;

namespace ATLConveyanceBill.Controllers
{
    public partial class ReportView : System.Web.UI.Page
    {
        ConveyanceManager conveyanceManager = new ConveyanceManager();
        EmployeeManager employeeManager = new EmployeeManager();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //var dateFrom =(DateTime) Session["dateFrom"];
            //var dateTo =(DateTime)Session["dateTo"] ;
            //var empId = (int)Session["empId"];
            //var check =(bool)Session["check"];
            //var denied = (bool)Session["approve"];
            //var unpaid = (bool)Session["pay"];
            //var conveyanceList = conveyanceManager.GetAllConveyanceViewModels().Where(a => a.Id == empId/* && a.Checked==check && a.Approved==denied*/ && a.Paid==unpaid);
            var conveyanceList = Session["Query"];
            
            ReportDocument reportDocument = new ReportDocument();
            
            reportDocument.Load(Path.Combine(Server.MapPath("~/Reports"), "ConveyanceReport.rpt"));
            reportDocument.SetDatabaseLogon("", "", @"DESKTOP-1TH4N6D\SQLEXPRESS", "ConveyanceDB");

            reportDocument.SetDataSource(conveyanceList);
            Voucher.ReportSource = reportDocument;
            Voucher.RefreshReport();
        }
    }
}