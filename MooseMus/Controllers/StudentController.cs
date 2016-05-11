using MooseMus.Models.ViewModels;
using MooseMus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooseMus.Controllers
{
    public class StudentController : Controller
    {
        private CourseService _cservice = new CourseService();
        private ProjectService _pservice = new ProjectService();
        private UserService _uservice = new UserService();

        // GET: Student
        public ActionResult Index(string course, int stuID)
        {
            var courseID = _cservice.getCourseIDByName(course);
            var model = _cservice.getCourseProjects(courseID);
            model.studentID = stuID;
            return View(model);
        }

        public ActionResult viewProject(int stuID, int projID)
        {
            var student = _uservice.getUserByID(stuID);
            var project = _pservice.getProjectByID(projID);
            List<SubmissionViewModel> part = _pservice.getBestSubmissionsByStudent(stuID, projID);
            var model = new TeacherProjectStudentViewModel()
            {
                studentName = student.name,
                projectName = project.title,
                parts = part
            };
            return PartialView(model);
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
    }


}