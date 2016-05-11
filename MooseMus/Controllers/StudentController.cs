using MooseMus.Models.ViewModels;
using MooseMus.Services;
using System;
using System.Collections.Generic;
using System.IO;
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
            List<SubmissionViewModel> part = _pservice.getBestSubmissionsAndNoSubByStudent(stuID, projID);
            var model = new TeacherProjectStudentViewModel()
            {
                studentName = student.name,
                projectName = project.title,
                parts = part
            };
            return PartialView(model);
        }

        public ActionResult viewProjectToSubmit(int stuID, int projID)
        {
            var student = _uservice.getUserByID(stuID);
            var project = _pservice.getProjectByID(projID);
            List<SubmissionViewModel> part = _pservice.getBestSubmissionsAndNoSubByStudent(stuID, projID);
            var model = new TeacherProjectStudentViewModel()
            {
                studentName = student.name,
                projectName = project.title,
                parts = part,
                description = project.description
            };
            return PartialView(model);
        }

        //Nemandi fer í skilasvæði
        public ActionResult submitAProjectPart(int stuID, int proParID)
        {
            var proPar = _pservice.getProjectPartByID(proParID);
            var model = new StudentSubmitViewModel()
            {
                studentID = stuID,
                projectPartID = proPar.ID,
                projectPartName = proPar.title,
                description = proPar.description
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult submitAProjectPart(StudentSubmitViewModel projParSent)
        {
            if (ModelState.IsValid)
            {
                // Skoða http://stackoverflow.com/questions/304617/html-helper-for-input-type-file
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    projParSent.file.InputStream.CopyTo(memoryStream);
                }
            }
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