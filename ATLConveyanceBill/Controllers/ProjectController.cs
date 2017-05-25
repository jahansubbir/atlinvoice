using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATLConveyanceBill.BLL;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.Controllers
{
    public class ProjectController : Controller
    {
        public int EId, LoginStatus, AdminId;
        private HttpContext a;
        private HttpCookie cookie;
        public ProjectController()
        {
           
            a=System.Web.HttpContext.Current;
            cookie = a.Request.Cookies["loginCookie"];
          //  string b,c,d;
            

            if (cookie!=null)
            {
                 LoginStatus= Convert.ToInt32(cookie["LoginStatus"]);
                 EId = Convert.ToInt32(cookie["EId"]);
                 AdminId = Convert.ToInt32(cookie["AdminId"]);
            }
            //LoginStatus = Convert.ToInt32(b);
            
             /*Convert.ToInt32(Request.Cookies.AllKeys.Contains("LogStatus"))*/ /*Convert.ToInt32(Session["LoginStatus"])*/;
            //EId = Convert.ToInt32(c);
            //AdminId = Convert.ToInt32(d);
        }
        ProjectManager projectManager=new ProjectManager();
        //
        // GET: /Project/
        public ActionResult Index()
        {
            if (LoginStatus!=0)
            {
                var projects = projectManager.GetProjects();
                return View(projects);
            }
            else
            {
                return RedirectToAction("GenerateError", "Login");
            }
        }

        //
        // GET: /Project/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //
        // GET: /Project/Create
        public ActionResult Create()
        {
            if (LoginStatus!=0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("GenerateError", "Login");
            }
        }

        //
        // POST: /Project/Create
        [HttpPost]
        public ActionResult Create(Project project)
        {
            try
            {
                // TODO: Add insert logic here
                ViewBag.Message = projectManager.CreateProject(project);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Project/Edit/5
        public ActionResult Edit(int id)
        {
            if (LoginStatus!=0)
            {
                var project = projectManager.GetProjects().Find(a => a.Id == id);
                return View(project);
            }
            else
            {
                return RedirectToAction("GenerateError", "Login");
            }
        }

        //
        // POST: /Project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Project project)
        {
            try
            {
                // TODO: Add update logic here
                var projects = projectManager.GetProjects().Find(a => a.Id == id);
                ViewBag.Message = projectManager.Edit(id, project);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(project);
            }
        }

        //
        // GET: /Project/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //
        // POST: /Project/Delete/5
        //[HttpPost]
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
    }
}
