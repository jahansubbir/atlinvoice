using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATLConveyanceBill.BLL;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.Controllers
{
    public class DesignationController : Controller
    {
        private int LoginStatus, EId, AdminId;
        private HttpContext a;
        private HttpCookie cookie;
        public DesignationController()
        {
            a = System.Web.HttpContext.Current;
            cookie = a.Request.Cookies["loginCookie"];
            if (cookie!=null)
            {
                LoginStatus = Convert.ToInt32(cookie["LoginStatus"]);
                EId = Convert.ToInt32(cookie["EId"]);
                AdminId = Convert.ToInt32(cookie["AdminId"]);
                
            }
        }
        DesignationManager designationManager=new DesignationManager();
        //
        // GET: /Designation/
        public ActionResult Index()
        {
            ViewBag.Designations = designationManager.GetDesignations();
            return View(ViewBag.Designations);
        }

        //
        // GET: /Designation/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //
        // GET: /Designation/Create
        public ActionResult Create()
        {
            if (LoginStatus!=0 && EId==AdminId)
            {
                return View();
            }
            else
            {
                return RedirectToAction("UnauthorizedMessage", "Login");
            }
            
        }

        //
        // POST: /Designation/Create
        [HttpPost]
        public ActionResult Create(Designation designation)
        {
            try
            {
                // TODO: Add update logic here
                ViewBag.Message = designationManager.CreateDesignation(designation);
                if (ViewBag.Message=="Created")
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
                // GET: /Designation/Edit/5
            
          public  ActionResult Edit(int id)
        {
            if (LoginStatus != 0 && EId == AdminId)
            {
                Designation designation = designationManager.GetDesignations().Find(a=>a.Id==id);
                return View(designation);
            }
            else
            {
                return RedirectToAction("UnauthorizedMessage", "Login");
            }
            
        }

        //
        // POST: /Designation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Designation designation)
        {
            try
            {
                // TODO: Add update logic here
                ViewBag.Message = designationManager.EditDesignation(designation);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Designation/Delete/5
        public ActionResult Delete(int id)
        {
            if (LoginStatus != 0 && EId == AdminId)
            {
                return View();
            }
            else return RedirectToAction("UnauthorizedMessage", "Login");

        }

        //
        // POST: /Designation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Designation designation)
        {
            try
            {
                // TODO: Add delete logic here
                designationManager.Delete(designation);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
