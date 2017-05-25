using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATLConveyanceBill.BLL;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeManager employeeManager = new EmployeeManager();
        DepartmentManager departmentManager = new DepartmentManager();
        DesignationManager designationManager = new DesignationManager();
        private int LoginStatus, EId, AdminId;
        private HttpContext context;
        private HttpCookie cookie;

        public EmployeeController()
        {

            context = System.Web.HttpContext.Current;
            cookie = context.Request.Cookies["loginCookie"];
            if (cookie!=null)
            {
                LoginStatus = Convert.ToInt32(cookie["LoginStatus"]);
                EId = Convert.ToInt32(cookie["EId"]);
                AdminId = Convert.ToInt32(cookie["AdminId"]);
            }
        }
        //
        // GET: /Employee/
        public ActionResult Index()
        {
            if (LoginStatus!=0)
            {
                if (EId==AdminId)
                {
                    GetLists();

                    return View();
    
                }
                return RedirectToAction("UnauthorizedMessage", "Login");
            }
            else
            {
                return RedirectToAction("GenerateError", "Login");
            }
        }

        //
        // GET: /Employee/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //
        // GET: /Employee/Create
        public ActionResult Create()
        {
                GetSelectList();
                return View();
            
            
        }

        //
        // POST: /Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            //GetSelectList();
            //ViewBag.Message = employeeManager.CreateAccount(employee);
            //return View();
            try
            {
                // TODO: Add insert logic here
                GetSelectList();

                ViewBag.Message = employeeManager.CreateAccount(employee);
                return View();
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Employee/Edit/5
        public ActionResult Edit(int id)
        {
            if (LoginStatus!=0)
            {
                GetSelectList();
                Employee employee = employeeManager.GetEmployees().Find(a=>a.Id==id);
                return View(employee);
            }
            else
            {
                return RedirectToAction("GenerateError", "Login");
            }
        }

        //
        // POST: /Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, Employee employee)
        {
            try
            {
                // TODO: Add update logic here
                GetSelectList();
                ViewBag.Message = employeeManager.EditProfile(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                // TODO: Add delete logic here


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void GetLists()
        {
            ViewBag.Employees = employeeManager.GetEmployees();
            ViewBag.Designations = designationManager.GetDesignations();
            ViewBag.Departments = departmentManager.GetDepartments();
        }

        public void GetSelectList()
        {
            ViewBag.Designations = new SelectList(designationManager.GetDesignations(), "Id", "Name");
            ViewBag.Departments = new SelectList(departmentManager.GetDepartments(), "Id", "Name");
        }

        public JsonResult GetEmployeeById(int empId)
        {
            List<Employee> employees = employeeManager.GetEmployees();
            var employeeList = employees.Where(a => a.Id == empId);
            return Json(employeeList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActivateUser()
        {
            if (LoginStatus != 0)
            {
                if (EId == AdminId)
                {
                    ViewBag.UnactivatedUser = employeeManager.GetInactiveEmployees();
                    ViewBag.Departments = departmentManager.GetDepartments();
                    ViewBag.Designations = designationManager.GetDesignations();
                    var inactive = ViewBag.UnactivatedUser;
                    return View(inactive);
                }
                else
                {
                   return RedirectToAction("UnauthorizedMessage", "Login");
                }
            }
            else
            {
                return RedirectToAction("GenerateError", "Login");
            }
        
        }

        [HttpPost]
        public ActionResult ActivateUser(Activate activate)
        {

            activate.Status = 1;

            ViewBag.Message = employeeManager.ActivateUser(activate);
            ViewBag.UnactivatedUser = employeeManager.GetInactiveEmployees();
            ViewBag.Departments = departmentManager.GetDepartments();
            ViewBag.Designations = designationManager.GetDesignations();
            var inactive = ViewBag.UnactivatedUser;
            return View(inactive);
        }
    }
}
