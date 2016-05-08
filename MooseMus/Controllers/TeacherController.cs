using MooseMus.Models.ViewModels;
using MooseMus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooseMus.Controllers
{
    public class TeacherController : Controller
    {
        private CourseService _cservice = new CourseService();

        // GET: Teacher
        public ActionResult Index(string course)
        {
            var courseID = _cservice.getCourseIDByName(course);
            var model = _cservice.getCourseProjects(courseID);
            return View(model);
        }

        //Kennari fer inná svæði til þess að bæta við verkefni
        public ActionResult addProject()
        {
            return View();
        }

        //Kennari fer inná svæði til þess að breyta verkefni
        public ActionResult goToEditProject()
        {
            return View();
        }

        //Kennari velur að bæta við verkefni
        public ActionResult goToAddProject(string course)
        {
            var courseId = _cservice.getCourseIDByName(course);
            var model = new TeacherAddEditViewModel()
            {
                courseID = courseId,
                projectID = 0,
                title = "",
                projectDescription = "",
                created = false
            };
            return PartialView("createProject", model);
        }

        [HttpPost]
        public ActionResult goToAddProject(TeacherAddEditViewModel project)
        {
            var models = new CourseProjectsViewModel()
            {
                projects = _cservice.getProjectsByCourse(project.courseID),
                created = true
            };
            return View("Index", models);
        }

        //Kennari bætir við verkefni
        public ActionResult projectAdded()
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

        public ActionResult viewProjectsByCourse()
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

        public ActionResult selectStudent()
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