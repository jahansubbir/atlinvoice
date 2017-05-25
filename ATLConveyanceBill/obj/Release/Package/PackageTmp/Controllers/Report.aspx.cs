using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATLConveyanceBill.BLL;
using ATLConveyanceBill.Models;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Ajax.Utilities;

namespace ATLConveyanceBill.Controllers
{
    public partial class Report : System.Web.UI.Page
    {
        ConveyanceManager conveyanceManager = new ConveyanceManager();
        EmployeeManager employeeManager=new EmployeeManager();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<Employee> employees = employeeManager.GetEmployees();
                //EmployeeDropDownList.DataSource = from i in employees
                //    select new ListItem()
                //    {
                //        Text = i.Name,
                //        Value = i.Id.ToString()
                //    };
                
                
                EmployeeDropDownList.DataSource = employees;
                
                EmployeeDropDownList.DataTextField = "Name";
                EmployeeDropDownList.DataValueField = "Id";
                //EmployeeDropDownList.DataTextField=employees.
                EmployeeDropDownList.DataBind();
               // EmployeeDropDownList.Items.FindByText("Select");

            }
            //
        }

        protected void ReportButton_Click(object sender, EventArgs e)
        {
            DateTime dateFrom;
            dateFrom = FromDateTextBox.Text!="" ? Convert.ToDateTime(FromDateTextBox.Text) : DateTime.MinValue;
           
            DateTime dateTo;
            dateTo= ToDateTextBox.Text !="" ? Convert.ToDateTime(ToDateTextBox.Text) : DateTime.MaxValue;
            int empId = Convert.ToInt32(EmployeeDropDownList.SelectedItem.Value);
            bool check = Convert.ToBoolean(CheckedCheckBox.Checked);
            bool approve = Convert.ToBoolean(ApprovedCheckBox.Checked);
            bool pay = Convert.ToBoolean(PaidCheckBox.Checked);
            bool all = Convert.ToBoolean(CheckAllCheckBox.Checked);
            var conveyances = conveyanceManager.GetAllConveyanceViewModels();
            
            var conveyanceList = conveyanceManager.GetAllConveyanceViewModels().Where(a => a.EmployeeId == empId && a.Date >= dateFrom && a.Date <= dateTo);
          
                if (check)
                {
                    conveyanceList = conveyanceList.Where(a => a.Checked=="NO");
                }
                if (approve)
                {
                    conveyanceList = conveyanceList.Where(a => a.Approved=="NO");
                }
                if (pay)
                {
                    conveyanceList = conveyanceList.Where(a => a.Paid=="NO");
                }

            if (all)
            {
                if (pay)
                {
                    Session["Query"] = conveyances.Where(a => a.Paid=="NO");
                }
                if (check)
                {
                    Session["Query"] = conveyances.Where(a => a.Checked=="NO");
                }
                if (approve)
                {
                    Session["Query"] = conveyances.Where(a => a.Approved=="NO");
                }
                else
                {
                    Session["Query"] = conveyances; 
                }
                
            }
           
            else
            {
                Session["Query"] = conveyanceList;
            }

            //IEnumerable<ConveyanceViewModel> asd = conveyanceList;
            
           
                Response.Redirect("ReportView.aspx");   
            
           
            
        }
           
            
            //Session["dateFrom"] = dateFrom;
            //Session["dateTo"] = dateTo;
            //Session["empId"] = empId;
            //Session["check"] = check;
            //Session["approve"] = approve;
            //Session["pay"] = pay;
            // var conveyanceList = conveyanceManager.GetAllConveyanceViewModels().Where(a=>a.Id==empId /*&& a.Checked==check && a.Approved==deny && a.Paid==unpaid*/);
            //ReportDocument reportDocument = new ReportDocument();

            //reportDocument.Load(Path.Combine(Server.MapPath("~/Reports"), "ConveyanceReport.rpt"));
            //reportDocument.SetDatabaseLogon("", "", @"DESKTOP-1TH4N6D\SQLEXPRESS", "ConveyanceDB");

            //reportDocument.SetDataSource(conveyanceList);
            //Voucher.ReportSource = reportDocument;
            //Voucher.RefreshReport();
        protected void VoucherButton_Click(object sender, EventArgs e)
        {
            DateTime dateFrom;
            dateFrom = FromDateTextBox.Text != "" ? Convert.ToDateTime(FromDateTextBox.Text) : DateTime.MinValue;

            DateTime dateTo;
            dateTo = ToDateTextBox.Text != "" ? Convert.ToDateTime(ToDateTextBox.Text) : DateTime.MaxValue;
            int empId = Convert.ToInt32(EmployeeDropDownList.SelectedItem.Value);
            bool check = Convert.ToBoolean(CheckedCheckBox.Checked);
            bool approve = Convert.ToBoolean(ApprovedCheckBox.Checked);
            bool pay = Convert.ToBoolean(PaidCheckBox.Checked);
            bool all = Convert.ToBoolean(CheckAllCheckBox.Checked);
            var conveyances = conveyanceManager.GetAllConveyanceViewModels();

            var conveyanceList = conveyanceManager.GetAllConveyanceViewModels().Where(a => a.EmployeeId == empId && a.Date >= dateFrom && a.Date <= dateTo);

            if (check)
            {
                conveyanceList = conveyanceList.Where(a => a.Checked=="NO");
            }
            if (approve)
            {
                conveyanceList = conveyanceList.Where(a => a.Approved=="NO");
            }
            if (pay)
            {
                conveyanceList = conveyanceList.Where(a => a.Paid=="NO");
            }

            if (all)
            {
                if (pay)
                {
                    Session["Query"] = conveyances.Where(a => a.Paid=="NO");
                }
                else if (check)
                {
                    Session["Query"] = conveyances.Where(a => a.Checked=="NO");
                }
                else if (approve)
                {
                    Session["Query"] = conveyances.Where(a => a.Approved=="NO");
                }
                else
                {
                    Session["Query"] = conveyances;
                }
            }
            else
            {
                Session["Query"] = conveyanceList;
            }

            //IEnumerable<ConveyanceViewModel> asd = conveyanceList;


            Response.Redirect("Voucher.aspx");   
        }
    }


      
    }
