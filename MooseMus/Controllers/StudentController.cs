using MooseMus.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooseMus.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult selectProject()
        {
            return View();
        }

        public ActionResult selectProjectPart()
        {
            return View();
        }

        //Nemandi fer í skilasvæði
        public ActionResult submitAProjectPart()
        {
            return View();
        }

        //Nemandi skilar inn
        public ActionResult submitProjectPart()
        {
            return View();
        }
        public ActionResult uploadProjectPart()
        {
            return View();
        }

        public ActionResult goBackToProjects()
        {
            return View();
        }

        public ActionResult goBackToProjectParts()
        {
            return View();
        }

        [HttpGet]
        public ActionResult submission()
        {
            return View();
        }

        [HttpPost]
        public ActionResult submission(StudentSubmitViewModel model)
        {

            return View();
        }




        public ActionResult goBackToCourses()
        {
            return View();
        }
    }


}