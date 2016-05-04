using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooseMus.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminView()
        {
            return View();
        }
        public ActionResult addUser()
        {
            return View();
        }

        public ActionResult editUser()
        {
            return View();
        }

        public ActionResult addCourse()
        {
            return View();
        }

        public ActionResult editCourse()
        {
            return View();
        }

        //Admin tengir nemanda við námskeið
        public ActionResult addStudent()
        {
            return View();
        }

        //Admin tengir kennara við námskeið
        public ActionResult addTeacher()
        {
            return View();
        }
    }
}