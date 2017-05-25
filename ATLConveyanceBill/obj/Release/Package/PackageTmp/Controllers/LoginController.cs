using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using ATLConveyanceBill.BLL;
using ATLConveyanceBill.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace ATLConveyanceBill.Controllers
{
    public class LoginController : Controller
    {

        //public static int eId, LoginStatus, AdminId ;
        LoginManager loginManager = new LoginManager();
        EmployeeManager employeeManager = new EmployeeManager();
        DepartmentManager departmentManager = new DepartmentManager();
        DesignationManager designationManager = new DesignationManager();
        //public HttpCookie loginCookie = new HttpCookie("LogStatus");
        HttpContext loginContext = System.Web.HttpContext.Current;
        private HttpCookie cookie;
        private int loginStatus, eId, adminId;

        public LoginController()
        {

        }


        //
        // GET: /Login/
        public ActionResult Index(int id)
        {
            cookie = loginContext.Request.Cookies["loginCookie"];
            if (cookie!=null)
            {
                loginStatus = Convert.ToInt32(cookie["LoginStatus"]);
                eId = Convert.ToInt32(cookie["EId"]);
                adminId = Convert.ToInt32(cookie["AdminId"]);
            }
           
            if (loginStatus != 0)
            {
                if (id == eId || id == adminId)
                {
                    ViewBag.Notification = employeeManager.ShowNewUser();
                    Session["Notification"] = ViewBag.Notification;
                    ViewBag.Employee = employeeManager.GetEmployeeById(id);
                    ViewBag.Departments = departmentManager.GetDepartments();
                    ViewBag.Designations = designationManager.GetDesignations();

                    return View();
                }
                else
                {
                    return RedirectToAction("UnauthorizedMessage");
                }

            }
            else
            {
                return RedirectToAction("GenerateError");
            }
        }

        public ActionResult AttemptLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AttemptLogin(Login login)
        {
            cookie =new HttpCookie("loginCookie");

            try
            {

                login.Date = DateTime.Now;
                loginManager.AttemptLogin(login);
                if (loginManager.AttemptLogin(login) == "Logged in Successfully")
                {
                    //Session["LoginStatus"] = 1;
                    cookie["LoginStatus"] = 1.ToString();
                    
                    //loginContext.Session["LoginStatus"] = 1;


                    if (loginManager.IsUser(login) == "Admin")
                    {
                        cookie["AdminId"] = employeeManager.GetEmployeeByUserId(login.UserId).Id.ToString();

                    }


                    cookie["EId"] = employeeManager.GetEmployeeByUserId(login.UserId).Id.ToString();

                    cookie.Expires = DateTime.Now.AddMonths(1);

                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Login", new { id = Convert.ToInt32(cookie["EId"]) });
                }
                else
                {
                    ViewBag.Message = loginManager.AttemptLogin(login);
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
                //throw;
            }
        }
        public ActionResult Logout(int? id)
        {
            return View();
        }

        //
        // POST: /Employee/Delete/5
        [HttpPost]
        public ActionResult Logout(int? id, Login login)
        {
            HttpCookie cookie = loginContext.Request.Cookies["loginCookie"];
            try
            {
                // TODO: Add delete logic here
                if (cookie!=null)
                {
                    cookie["LoginStatus"] = 0.ToString();
                    cookie["EId"] = 0.ToString();
                    cookie["AdminId"] = 0.ToString();
                    cookie.Expires = DateTime.Now.AddMonths(1);

                    Response.Cookies.Add(cookie);
                }
               

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GenerateError()
        {
            return View();
        }

        public ActionResult UnauthorizedMessage()
        {

            return View();

        }

    }
}