using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATLConveyanceBill.BLL;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.Controllers
{
    public class DepartmentController : Controller
    {
        private DepartmentManager departmentManager = new DepartmentManager();
        private int LoginStatus, EId, AdminId;
        private HttpContext context;
        private HttpCookie cookie;
        public DepartmentController()
        {
            context = System.Web.HttpContext.Current;
            cookie = context.Request.Cookies["loginCookie"];
            if (cookie!=null)
            {
                LoginStatus = Convert.ToInt32(cookie["LoginStatus"]);
                EId = Convert.ToInt32(cookie["EId"]);
                AdminId = Convert.ToInt32(cookie["AdminId"]);

            }
        } //
        // GET: /Department/
        public ActionResult Index()
        {
            ViewBag.Departments = departmentManager.GetDepartments();
            return View();
        }

        //
        // GET: /Department/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //
        // GET: /Department/Create
        public ActionResult Create()
        {
            if ( LoginStatus != 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("UnauthorizedMessage", "Login");
            }
        }

        //
        // POST: /Department/Create
        [HttpPost]
        public ActionResult Create(Department department)
        {
            
            try
            {
                // TODO: Add insert logic here
                ViewBag.Message = departmentManager.CreateDepartment(department);
                if (ViewBag.Message=="A new department Named " + department.Name + " has been created")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

               
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Department/Edit/5
        public ActionResult Edit(int id)
        {
            if (LoginStatus != 0)
            {
                Department department = departmentManager.IsExist(id);
                return View(department);
            }
            else
            {
                return RedirectToAction("UnauthorizedMessage", "Login");
            }
            
        }

        //
        // POST: /Department/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Department department)
        {
            try
            {
                // TODO: Add update logic here
                departmentManager.EditDepartment(department);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Department/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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

        public JsonResult GetDepartmentById(int deptId)
        {
            var department = departmentManager.IsExist(deptId);
            return Json(department, JsonRequestBehavior.AllowGet);
        }
    }
}
