using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooseMus.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //Kennari fer inná svæði til þess að bæta við eða breyta verkefni
        public ActionResult addEditProject()
        {
            return View();
        }

        //Kennari velur að bæta við verkefni
        public ActionResult addProject()
        {
            return View();
        }

        //Kennari bætir við verkefni
        public ActionResult projectAdd()
        {
            return View();
        }

        //Kennari velur að breyta verkefni
        public ActionResult editProject()
        {
            return View();
        }

        //Kennari velur verkefni til að breyta
        public ActionResult selectProjectToEdit()
        {
            return View();
        }

        //Kennari breytir verkefni
        public ActionResult projectEdit()
        {
            return View();
        }

        public ActionResult viewProjectByStudent()
        {
            return View();
        }

        public ActionResult selectProject()
        {
            return View();
        }

        public ActionResult selectSTudent()
        {
            return View();
        }

        public ActionResult selectProjectPart()
        {
            return View();
        }

        public ActionResult goBackHome()
        {
            return View();
        }

        public ActionResult goBackToCourses()
        {
            return View();
        }

        public ActionResult goBackToProjects()
        {
            return View();
        }

        public ActionResult goBackToStudents()
        {
            return View();
        }

        public ActionResult goBackToProject()
        {
            return View();
        }
    }
}