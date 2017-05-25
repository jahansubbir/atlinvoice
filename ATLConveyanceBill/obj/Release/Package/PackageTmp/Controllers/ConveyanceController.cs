using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using ATLConveyanceBill.BLL;
using ATLConveyanceBill.Gateway;
using ATLConveyanceBill.Models;

using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Ajax.Utilities;
using Rotativa;

namespace ATLConveyanceBill.Controllers
{
    public class ConveyanceController : Controller
    {
        /* updated secured deployed file*/


        //public object employee;
        //public object bill;
        private DateTime dateFrom;
        private DateTime dateTo;
        private int employeeId;
        private EmployeeManager employeeManager = new EmployeeManager();
        private ConveyanceManager conveyanceManager = new ConveyanceManager();
        private ProjectManager projectManager = new ProjectManager();
        //
        WorkLogManager workLogManager=new WorkLogManager();
        private int LoginStatus, EId, AdminId;
        private HttpContext info;
        private HttpCookie cookie;
        public ConveyanceController()
        {
            info = System.Web.HttpContext.Current;
            cookie = info.Request.Cookies["loginCookie"];
            if (cookie!=null)
            {
                LoginStatus = Convert.ToInt32(cookie["LoginStatus"]);
                EId = Convert.ToInt32(cookie["EId"]);
                AdminId = Convert.ToInt32(cookie["AdminId"]);
            }
           
        }

        // GET: /Conveyance/
        public ActionResult Index(int? id)
        {
            int empId = 0;
            if (id != null)
            {
                empId = (int) id;
            }

            if (Convert.ToInt32(cookie["AdminId"]) != 0 && id == Convert.ToInt32(cookie["EId"]))
            {
                ViewBag.Projects = projectManager.GetProjects();
                ViewBag.Employees = employeeManager.GetEmployees().Where(a => a.Id == id);
                ViewBag.Conveyances = conveyanceManager.GetConveyances(empId);
                return View();
            }
            else if (Convert.ToInt32(cookie["LoginStatus"]) != 0 &&
                     Convert.ToInt32(cookie["EId"]) == Convert.ToInt32(cookie["AdminId"]))
            {
                ViewBag.Projects = projectManager.GetProjects();
                ViewBag.Employees = employeeManager.GetEmployees().Where(a => a.Id == id);
                ViewBag.Conveyances = conveyanceManager.GetConveyances(empId);
                ViewBag.WorkLogs = workLogManager.GetWorkLogViewModels();
                return View();
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Conveyance/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //
        // GET: /Conveyance/Create

        public ActionResult Create(int id)
        {
            if (LoginStatus != 0)
            {
              
                    GetworkList(id); 
                
              
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Conveyance/Create
        [HttpPost]
        public ActionResult Create(int id, Conveyance conveyance,WorkLog log)
        {

            // TODO: Add insert logic here
            try
            {
                conveyance.EmployeeId = id;
                log.EmployeeId = id;
               
                GetworkList(id);
                WorkLogNo(conveyance,log);
                ViewBag.WorkLog = workLogManager.LogWork(log);
                ViewBag.Message = conveyanceManager.RequestBill(conveyance);
                ViewBag.Conveyances = conveyanceManager.GetConveyances(id).OrderByDescending(a=>a.Date);
                
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
                //throw;
            }


        }

        void WorkLogNo(Conveyance conveyance, WorkLog log)
        {
            log.WorkLogNo = "WL-" + log.WorkDate.ToString("yyMMdd") + "-" + log.ProjectId + EId;
            conveyance.Date = log.WorkDate;
            conveyance.ProjectId = log.ProjectId;
            conveyance.WorkLogNo = log.WorkLogNo;
            
        }

        void GetworkList(int id)
        {
            ViewBag.Conveyances = conveyanceManager.GetConveyances(EId).OrderByDescending(a=>a.Date);
            ViewBag.Project = projectManager.GetProjects();
            ViewBag.Employees = employeeManager.GetEmployees().Where(a => a.Id == id);
            ViewBag.WorkTypes = new SelectList(workLogManager.GetWorkTypes(), "Id", "Type");
            ViewBag.WorkSystems = new SelectList(workLogManager.GetWorkSystems(), "Id", "Name");
            SelectList ProjectList = new SelectList(projectManager.GetProjects(), "Id", "Name");
            ViewBag.Projects = ProjectList;
        }
        //
        // GET: /Conveyance/Edit/5
        public ActionResult Edit(int id)
        {
            GetworkList(id);
            string workLogNo = conveyanceManager.GetAllConveyances().Find(a => a.Id == id).WorkLogNo;
            var workLog = workLogManager.GetWorkLogs().Find(a => a.WorkLogNo == workLogNo);
            ViewBag.Conveyances = conveyanceManager.GetAllConveyances().Find(a => a.Id == id);
            SelectList ProjectList = new SelectList(projectManager.GetProjects(), "Id", "Name");
            ViewBag.Projects = ProjectList;
            var conv = ViewBag.Conveyances;

           TotalConveyanceInfo totalInfo=new TotalConveyanceInfo();
            totalInfo.WorkLog = workLog;
            totalInfo.Conveyance = conv;
            return View(totalInfo);
        }

        //
        // POST: /Conveyance/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Conveyance conveyance,WorkLog workLog)
        {
            try
            {
                // TODO: Add update logic here

                conveyance.EmployeeId = EId;
                conveyance.Id = id;
                workLog.EmployeeId = EId;
                GetworkList(id);
                string workLogNo = conveyanceManager.GetAllConveyances().Find(a => a.Id == id).WorkLogNo;
                workLog.Id = workLogManager.GetWorkLogs().Find(a => a.WorkLogNo == workLogNo).Id;
                ViewBag.Conveyances = conveyanceManager.GetAllConveyances().Find(a => a.Id == id);
                var conv = ViewBag.Conveyances;
                SelectList ProjectList = new SelectList(projectManager.GetProjects(), "Id", "Name");
                ViewBag.Projects = ProjectList;
                conveyance.Date = workLog.WorkDate;
                WorkLogNo(conveyance,workLog);
                ViewBag.Message = conveyanceManager.EditBill(conveyance,workLog);
                if (ViewBag.Message == "Edited")
                {
                    return RedirectToAction("Create", new {id = EId});
                }
                else
                {
                    return View();
                }

            }
            catch(Exception exception)
            {
                ViewBag.Message = exception.Message;
                return View();
            }
        }

        //
        // GET: /Conveyance/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //
        // POST: /Conveyance/Delete/5
        [HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //public ActionResult AuthenticateBill()

        public JsonResult GetEmployeeById(int empId)
        {
            var employee = employeeManager.GetEmployeeById(empId);
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AuthenticateBill(int id)
        {
            if (LoginStatus != 0 && EId == AdminId)
            {
                var conveyance = conveyanceManager.IsExist(id);
                int employeeId = conveyance.EmployeeId;
                ViewBag.Employee = employeeManager.GetEmployeeById(employeeId);

                return View(conveyance);
            }
            return RedirectToAction("Index", new {id = EId});
        }

        [HttpPost]
        public ActionResult AuthenticateBill(int id, Authenticate authenticate)
        {
            try
            {

                var conveyance = conveyanceManager.IsExist(id);
                int empId = conveyance.EmployeeId;

                if (conveyance.Checked)
                {
                    authenticate.Check = true;
                }
                if (conveyance.Approved)
                {
                    authenticate.Approve = true;
                }
                if (conveyance.Paid)
                {
                    authenticate.Pay = true;
                }
                ViewBag.Employee = employeeManager.GetEmployeeById(empId);
                ViewBag.Message = conveyanceManager.AuthenticateBill(id, authenticate);
                //todo;
                return RedirectToAction("Index", new {id = empId});

            }
            catch (Exception)
            {
                var conveyance = conveyanceManager.IsExist(id);
                return View(conveyance);
                //throw;
            }
        }

        public JsonResult GetConveyanceInfoById(int billId)
        {

            ViewBag.Employees = new SelectList(employeeManager.GetEmployees(), "Id", "Name");
            var conveyance = conveyanceManager.GetConveyanceByBillId(billId);
            return Json(conveyance, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ViewBill(int? id)
        {

            if (LoginStatus != 0 && EId == id)
            {
                ViewBag.Employees = employeeManager.GetEmployees().Find(a => a.Id == id);
                conveyanceManager.GetAllConveyanceViewModels().Where(a => a.Id == id);
                return View();
            }
            else
            {
                return RedirectToAction("UnauthorizedMessage", "Login");
            }
        }

        [HttpPost]
        public ActionResult ViewBill(int id, Authenticate authenticate)
        {
            ViewBag.Employees = employeeManager.GetEmployees().Find(a => a.Id == id);
            ViewBag.Message = conveyanceManager.PayBill(authenticate);
            return View();

        }

        public JsonResult GetConveyanceByDate(DateTime dateFrom, DateTime dateTo)
        {
            var conveyances = conveyanceManager.GetAllConveyanceViewModels();
            var conveyanceList = conveyances.Where(a => a.Date >= dateFrom && a.Date <= dateTo && a.EmployeeId == EId);
            //var aconveyance = conveyances.Where(a => a.Id == '3').Sum(a => a.Amount);
            //var n = new {x=conveyanceList, y= aconveyance};
            return Json(conveyanceList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetConveyanceByName(int empId)
        {
            var conveyances = conveyanceManager.GetAllConveyanceViewModels();
            var conveyanceList = conveyances.Where(a => a.Id == empId);
            return Json(conveyanceList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ViewAll()
        {
            var totalBills = conveyanceManager.ViewTotalBill();
            return Json(totalBills, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetConveyanceByChecking(bool check)
        {
            var conveyances = conveyanceManager.GetAllConveyanceViewModels();
            IEnumerable<ConveyanceViewModel> conveyanceList = conveyances;
            if (check)
            {
                conveyanceList = conveyances.Where(a => a.Checked=="YES");
            }
            else
            {
                conveyanceList = conveyances.Where(a => a.Checked=="NO");
            }

            return Json(conveyanceList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotalBillOfAnEmployee(int empId)
        {
            var totalBills = conveyanceManager.ViewTotalBill();
            var billOfOne = totalBills.Where(a => a.Id == empId);
            return Json(billOfOne, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotalBillOfAnEmployeeBetweenDates(int empId, DateTime dateFrom, DateTime dateTo)
        {
            //ConveyanceViewModel conveyanceViewModel=new ConveyanceViewModel();
            var totalBills = conveyanceManager.GetAllConveyanceViewModels();
            var billOfOne =
                totalBills.Where(a => a.Id == empId && a.Date >= dateFrom && a.Date <= dateTo).Sum(a => a.Amount);
            //var totalAmount = (from conveyanceViewModel in billOfOne select conveyanceViewModel.Amount).Sum();

            return Json(billOfOne, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBills(DateTime dateFrom, DateTime dateTo, int empId)
        {
            var conveyances = conveyanceManager.GetAllConveyanceViewModels();
            var conveyanceList = conveyances.Where(a => a.Date >= dateFrom && a.Date <= dateTo && a.EmployeeId == empId);
            var amount =
                conveyances.Where(a => a.Date >= dateFrom && a.Date <= dateTo && a.EmployeeId == empId)
                    .Sum(a => a.Amount);

            return Json(new {bill = conveyanceList, total = amount}, JsonRequestBehavior.AllowGet);

        }

        public JsonResult BillByDate(DateTime dateFrom, DateTime dateTo)
        {
            var conveyances = conveyanceManager.GetAllConveyanceViewModels();
            var conveyanceList = conveyances.Where(a => a.Date >= dateFrom && a.Date <= dateTo).OrderBy(a=>a.EmpId);
            var amount =
                conveyances.Where(a => a.Date >= dateFrom && a.Date <= dateTo)
                    .Sum(a => a.Amount);

            return Json(new { bill = conveyanceList, total = amount }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ViewBills(DateTime dateFrom, DateTime dateTo)
        {
            var conveyances = conveyanceManager.GetAllConveyanceViewModels().Where(a => a.EmployeeId == EId);
            var conveyanceList = conveyances.Where(a => a.Date >= dateFrom && a.Date <= dateTo && a.Paid=="NO");
            var amount = conveyances.Where(a => a.Date >= dateFrom && a.Date <= dateTo && a.Paid=="NO").Sum(a => a.Amount);

            return Json(new {bill = conveyanceList, total = amount}, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ViewUnchecked(DateTime dateFrom, DateTime dateTo)
        {
            var conveyances = conveyanceManager.GetAllConveyanceViewModels().Where(a => a.EmployeeId == EId);
            var conveyanceList = conveyances.Where(a => a.Date >= dateFrom && a.Date <= dateTo && a.Paid=="NO");
            var amount = conveyances.Where(a => a.Date >= dateFrom && a.Date <= dateTo).Sum(a => a.Amount);

            return Json(new {bill = conveyanceList, total = amount}, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ApproveBill()
        {
            if (LoginStatus != 0 && EId == AdminId)
            {
                ViewBag.Employees = employeeManager.GetEmployees();
                conveyanceManager.GetAllConveyanceViewModels();
                return View();
            }
            else
            {
                return RedirectToAction("UnauthorizedMessage", "Login");
            }
        }

        [HttpPost]
        public ActionResult ApproveBill(Authenticate authenticate)
        {
            ViewBag.Employees = employeeManager.GetEmployees();
            ViewBag.Message = conveyanceManager.ApproveBill(authenticate);
            return View();
        }

        public ActionResult CheckBill()
        {
            if (LoginStatus != 0 && EId == AdminId)
            {
                ViewBag.Employees = employeeManager.GetEmployees();
                conveyanceManager.GetAllConveyanceViewModels();
                return View();
            }
            else
            {
                return RedirectToAction("UnauthorizedMessage", "Login");
            }
        }

        [HttpPost]
        public ActionResult CheckBill(Authenticate authenticate)
        {
            ViewBag.Employees = employeeManager.GetEmployees();
            ViewBag.Message = conveyanceManager.CheckBill(authenticate);
            return View();
        }

        public ActionResult PayBill()
        {
            if (LoginStatus != 0 && EId == AdminId)
            {
                ViewBag.Employees = employeeManager.GetEmployees();
                conveyanceManager.GetAllConveyanceViewModels();
                return View();
            }
            else
            {
                return RedirectToAction("UnauthorizedMessage", "Login");
            }
        }

        [HttpPost]
        public ActionResult PayBill(Authenticate authenticate)
        {
            ViewBag.Employees = employeeManager.GetEmployees();
            ViewBag.Message = conveyanceManager.PayBill(authenticate);
            return View();
        }

        public ActionResult DownloadViewPDF(DateTime dateFrom, DateTime dateTo, int employeeId)
        {


            return new ActionAsPdf("ViewPDF", new {dF = dateFrom, dT = dateTo, empId = employeeId})
            {
                FileName = Server.MapPath("~/Rotativa/Downloads/Bill.pdf")
            };
        }

        public ActionResult ViewPDF(DateTime dF, DateTime dT, int empId)
        {

            ViewBag.Employees = employeeManager.GetEmployees();
            ViewBag.Bills = conveyanceManager.GetSpecificBill(dF, dT, empId);
            var bill = ViewBag.Bills;
            ViewBag.Total = conveyanceManager.CalculateTotalAmount(dF, dT, empId);
            return View(bill);
        }

        public ActionResult LoadBill()
        {
            ViewBag.Bills = null;
            ViewBag.Employees = employeeManager.GetEmployees();
            return View();
        }

        [HttpPost]
        public ActionResult LoadBill(Authenticate authenticate)
        {
            dateFrom = authenticate.From;
            dateTo = authenticate.To;
            employeeId = authenticate.EmployeeId;
            //ViewBag.Employees = employeeManager.GetEmployees();
            //ViewBag.Bills = conveyanceManager.GetSpecificBill(authenticate.From,authenticate.To,authenticate.EmployeeId);


            return RedirectToAction("DownloadViewPDF",
                new {dateFrom = authenticate.From, dateTo = authenticate.To, employeeId = authenticate.EmployeeId});

        }

        public ActionResult ViewWorkLog(int? id)
        {
            ViewBag.Employee = new SelectList(employeeManager.GetEmployees(),"Id","Name");
            return View();
        }

        [HttpPost]
        public ActionResult ViewWorkLog(WorkLog work)
        {
            ViewBag.Employee = new SelectList(employeeManager.GetEmployees(), "Id", "Name");
            var log = workLogManager.GetWorkLogViewModels();
            return View(log);
        }


        public ActionResult ExportBill()
        {
            var conveyanceList = conveyanceManager.GetAllConveyanceViewModels();
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(Path.Combine(Server.MapPath("~/Reports"), "ConveyanceReport.rpt"));
            reportDocument.SetDataSource(conveyanceList);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ConveyanceVoucher.pdf");
        }

        public ActionResult ViewReport()
        {
            //var conveyanceList = conveyanceManager.GetAllConveyanceViewModels().Where(a => a.From == dateFrom);


            var conveyanceList = conveyanceManager.GetAllConveyanceViewModels();
            ViewBag.Employees = employeeManager.GetEmployees();
            return View();
        }

        [HttpPost]
        public ActionResult ViewReport(Authenticate authenticate)
        {
            ViewBag.Employees = employeeManager.GetEmployees();
            return
                Redirect("~/Controllers/Report.aspx?fromDate=" + authenticate.From + "&toDate=" + authenticate.To +
                         "&empId=" + authenticate.EmployeeId + "&unChecked=" + authenticate.Check);
        }

        public ActionResult ApproveSelectedBills()
        {
            if (LoginStatus != 0 && EId == AdminId)
            {
                ViewBag.Employees = employeeManager.GetEmployees();
                conveyanceManager.GetAllConveyanceViewModels();
                return View();
            }
            else
            {
                return RedirectToAction("UnauthorizedMessage", "Login");
            }
        }

        [HttpPost]
        public ActionResult ApproveSelectedBills(Approval approval)
        {
            ViewBag.Employees = employeeManager.GetEmployees();
            ViewBag.Message = conveyanceManager.ApproveSelectedBills(approval);
            return View();
        }

        public ActionResult CheckSelectedBills()
        {
            if (LoginStatus != 0 && EId == AdminId)
            {
                ViewBag.Employees = employeeManager.GetEmployees();
                conveyanceManager.GetAllConveyanceViewModels();
                return View();
            }
            else
            {
                return RedirectToAction("UnauthorizedMessage", "Login");
            }
        }

        [HttpPost]
        public ActionResult CheckSelectedBills(Approval approval)
        {
            ViewBag.Employees = employeeManager.GetEmployees();
            conveyanceManager.GetAllConveyanceViewModels();
            ViewBag.Message = conveyanceManager.CheckSelectedBills(approval);
            return View();
        }

        public ActionResult PaySelectedBills()
        {
            if (LoginStatus != 0 && EId == AdminId)
            {
                ViewBag.Employees = employeeManager.GetEmployees();
                conveyanceManager.GetAllConveyanceViewModels();
                return View();
            }
            else
            {
                return RedirectToAction("UnauthorizedMessage", "Login");
            }
        }

        [HttpPost]
        public ActionResult PaySelectedBills(Approval approval)
        {
            ViewBag.Employees = employeeManager.GetEmployees();
            conveyanceManager.GetAllConveyanceViewModels();
            ViewBag.Message = conveyanceManager.PaySelectedBills(approval);
            return View();
        }
        public ActionResult Report()
        {
            if (LoginStatus != 0 && EId!=0)
            {
                ViewBag.Employees = new SelectList(employeeManager.GetEmployees(), "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("UnauthorizedMessage", "Login");
            }
        }

        [HttpPost]
        public ActionResult Report(Authenticate authenticate, int? Checked, int? Approved, int? Paid, int? Summery, int? WorkLog)
        {
            string period = "From " + authenticate.From.ToString("dd-MMM-yy") + " To " + authenticate.To.ToString("dd-MMM-yy");

            ViewBag.Employees = new SelectList(employeeManager.GetEmployees(), "Id", "Name");
            var conveyances = conveyanceManager.GetAllConveyanceViewModels()
                      .Where(a => a.Date >= authenticate.From && a.Date <= authenticate.To);

            if (EId!=AdminId)
            {
                conveyances = conveyances.Where(a => a.EmployeeId == EId);
            }
            if (Checked == 1)
            {
                conveyances = conveyances.Where(a => a.Checked=="YES");
            }
            else if (Checked == 2)
            {
                conveyances = conveyances.Where(a => a.Checked=="NO");
            }


            if (Approved == 1)
            {
                conveyances = conveyances.Where(a => a.Approved=="YES");
            }
            else if (Approved == 2)
            {
                conveyances = conveyances.Where(a => a.Approved=="NO");
            }
            if (Paid == 1)
            {
                conveyances = conveyances.Where(a => a.Paid=="YES");
            }
            else if (Paid == 2)
            {
                conveyances = conveyances.Where(a => a.Paid=="NO");
            }
            if (authenticate.EmployeeId != 0)
            {
                conveyances = conveyances.Where(a => a.EmployeeId == authenticate.EmployeeId);
            }

            if (Summery!=1)
            {
                ReportDocument reportDocument = new ReportDocument();
                reportDocument.Load(Path.Combine(Server.MapPath("~/Reports"), "ConveyanceReport.rpt"));
                reportDocument.SetDataSource(conveyances);
                reportDocument.DataDefinition.FormulaFields["UnboundString1"].Text = "'" + period + "'";
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                // Stream stream1 = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream, "application/pdf", "Report" + DateTime.Today.ToString("dd-MMM-yy-") + EId + ".pdf");
            }
            else
            {
                ReportDocument reportDocument = new ReportDocument();
                reportDocument.Load(Path.Combine(Server.MapPath("~/Reports"), "ConveyanceSummery.rpt"));
                
                reportDocument.SetDataSource(conveyances);
                              
                reportDocument.DataDefinition.FormulaFields["UnboundString1"].Text = "'"+ period  +"'";
               // reportDocument.DataDefinition.FormulaFields["To"].Text = "bcv";
                
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                // Stream stream1 = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream, "application/pdf", "Summery" + DateTime.Today.ToString("dd-MMM-yy") + EId + ".pdf");
            }

            
            
           
        }

        public ActionResult WorkLogReport()
        {
            if (LoginStatus != 0 && EId != 0)
            {
                ViewBag.Employees = new SelectList(employeeManager.GetEmployees(), "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("UnauthorizedMessage", "Login");
            }
        }

        [HttpPost]
        public ActionResult WorkLogReport(int? EmployeeId, DateTime From, DateTime To)
        {
            ViewBag.Employees = new SelectList(employeeManager.GetEmployees(), "Id", "Name");
            var WorkLogs = workLogManager.GetWorkLogViewModels();
            string period = "From " + From.ToString("dd-MMM-yy") + " To " + To.ToString("dd-MMM-yy");
            if (EmployeeId!=null)
            {
                WorkLogs = WorkLogs.Where(a => a.EmployeeId == EmployeeId).ToList();
            }
            if (From!=null)
            {
                WorkLogs = WorkLogs.Where(a => a.Date >= From).ToList();
            }
            if (To!=null)
            {
                WorkLogs = WorkLogs.Where(a => a.Date <= To).ToList();
            }

            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(Path.Combine(Server.MapPath("~/Reports"), "WorkLogReport.rpt"));
            reportDocument.SetDataSource(WorkLogs);
            reportDocument.DataDefinition.FormulaFields["UnboundString1"].Text = "'" + period + "'";
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            // Stream stream1 = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "WorkLog" + DateTime.Today.ToString("ddmmyy-") + EId + ".pdf");
            
           
        }

        public JsonResult ViewLog(DateTime fromDate, DateTime toDate)
        {
            var workLog = workLogManager.GetWorkLogViewModels();
            
                workLog =
                    workLog.Where(a => a.Date >= fromDate && a.Date <= toDate).OrderBy(a=>a.Date).ToList();
            return Json(workLog, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ViewSpecificLog(DateTime fromDate, DateTime toDate, int empId)
        {
            var workLog = workLogManager.GetWorkLogViewModels();

            workLog =
               workLog.Where(a => a.Date >= fromDate && a.Date <= toDate && a.EmployeeId == empId).OrderBy(a => a.Date).ToList();
            return Json(workLog, JsonRequestBehavior.AllowGet);
        }

        
    }
}

