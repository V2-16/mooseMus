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
        private ProjectService _pservice = new ProjectService();
        private UserService _uservice = new UserService();

        // GET: Teacher
        public ActionResult Index(string course)
        {
            ViewBag.Success = false;
            var courseID = _cservice.getCourseIDByName(course);
            var model = _cservice.getCourseProjects(courseID);
            return View(model);
        }

        //Kennari fer inná svæði til þess að bæta við verkefni
        public ActionResult addProjectPart()
        {
            return PartialView("ProjectPartCreated");
        }

        //Kennari fer inná svæði til þess að breyta verkefni
        public ActionResult goToEditProject()
        {
            return View();
        }

        //Kennari velur að bæta við verkefni
        public ActionResult goToAddProject(string course)
        {
            ViewBag.Success = false;
            var courseId = _cservice.getCourseIDByName(course);
            var model = new TeacherAddEditViewModel()
            {
                courseID = courseId,
                projectID = 0,
                title = "",
                projectDescription = "",
            };
            return PartialView("createProject", model);
        }

        [HttpPost]
        public ActionResult goToAddProject(TeacherAddEditViewModel project)
        {
            
            ViewBag.Success = true;
            var model = _cservice.getCourseProjects(project.courseID);
            _pservice.addProject(project);
            return View("Index", model);
        }

        public ActionResult createProjectPart(string course)
        {
            var cID = _cservice.getCourseIDByName(course);
            var model = new TeacherAddProjectPartViewModel()
            {
                courseID = cID,
                projects = _cservice.getProjectsByCourse(cID),
                partName = "",
                partDescription = "",
                input = "",
                output = ""
            };
            return PartialView("CreateProjectPart", model);
        }

        [HttpPost]
        public ActionResult createProjectPart(TeacherAddProjectPartViewModel part)
        {
            ViewBag.Success = true;
            _pservice.addProjectPart(part);
            string courseName = _cservice.getCourseNameByID(part.courseID);
            return RedirectToAction("Index", "Teacher", new { course = courseName });
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

        public ActionResult viewStudentsByProject(string projectName)
        {
            var model = new TeacherProjectViewModel()
            {
                project = projectName,
                students = _pservice.getStudentsInProject(projectName)
            };
            return PartialView(model);
        }

        public ActionResult viewProjectByStudent(int studentID)
        {
            string projName = "Verkefni 1";
            var student = _uservice.getUserByID(studentID);
            List<SubmissionViewModel> part = _pservice.getBestSubmissionsByStudent(studentID, projName);
            var model = new TeacherProjectStudentViewModel()
            {
                studentName = student.name,
                projectName = projName,
                parts = part
            };
            return PartialView(model);
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