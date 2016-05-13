using MooseMus.Models.ViewModels;
using MooseMus.Services;
using System.Collections.Generic;
using System.Web.Mvc;
using MooseMus.Handlers;

namespace MooseMus.Controllers
{
    [CustomHandleError]
    public class TeacherController : Controller
    {
        private CourseService _cservice = new CourseService();
        private ProjectService _pservice = new ProjectService();
        private UserService _uservice = new UserService(null);

        // GET: Teacher
        public ActionResult Index(string course)
        {
            var courseID = _cservice.getCourseIDByName(course);
            var model = _cservice.getCourseProjects(courseID);
            return View(model);
        }

        /********************** Teacher views projects *********************/

        // Teacher views all projectparts
        public ActionResult viewProjectPartByStudent(int studentID, int projectPartID)
        {
            var project = _pservice.getProjectPartByID(projectPartID);
            var student = _uservice.getUserByID(studentID);
            List<SubmissionViewModel> sub = _pservice.getSubmissionsByStudentAndPart(studentID, projectPartID);
            var model = new StudentProjectPartViewModel()
            {
                partName = project.title,
                projectPartID = projectPartID,
                studentName = student.name,
                submissions = sub
            };
            return PartialView(model);
        }

        // Teacher views all students that have submitted given projects
        public ActionResult viewStudentsByProject(int projectID)
        {
            var pro = _pservice.getProjectByID(projectID);
            var model = new TeacherProjectViewModel()
            {
                projectID = projectID,
                project = pro.title,
                students = _pservice.getStudentsInProject(projectID)
            };
            return PartialView(model);
        }

        // Teacher views best submitted projects
        public ActionResult viewProjectByStudent(int studentID, int projID)
        {
            var student = _uservice.getUserByID(studentID);
            var project = _pservice.getProjectByID(projID);
            List<SubmissionViewModel> part = _pservice.getBestSubmissionsByStudent(studentID, projID);
            var model = new TeacherProjectStudentViewModel()
            {
                studentName = student.name,
                projectName = project.title,
                parts = part
            };
            return PartialView(model);
        }

        /********************** Teacher adds a project *********************/

        // Teacher adds a project
        public ActionResult createProject(string course)
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
        public ActionResult createProject(TeacherAddEditViewModel project)
        {

            ViewBag.Success = true;
            _pservice.addProject(project);
            var model = _cservice.getCourseProjects(project.courseID);
            return View("Index", model);
        }

        /********************** Teacher adds a projectpart *********************/

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
            var model = _cservice.getCourseProjects(part.courseID);
            return View("Index", model);
        }

        /********************** Teacher edits project *********************/

        // Teacher edits project
        public ActionResult projectSelectedToEdit(int projID)
        {
            var model = _pservice.getProjectPartsByID(projID);
            return PartialView(model);
        }

        public ActionResult editProjectPart(int projParID)
        {
            var ppart = _pservice.getProjectPartByID(projParID);
            var model = new TeacherAddProjectPartViewModel()
            {
                ID = ppart.ID,
                projectID = ppart.projectID,
                partName = ppart.title,
                partDescription = ppart.description,
                input = ppart.input,
                output = ppart.output
            };
            return PartialView(model);
        }

        // Teacher edits projectpart
        public ActionResult updateProjectPart(TeacherAddProjectPartViewModel toEdit)
        {
            ViewBag.Success = true;
            if (ModelState.IsValid)
            {
                _pservice.updateProjectPart(toEdit);
            }
            var course = _pservice.getCourseByProjectID(toEdit.projectID);
            var model = _cservice.getCourseProjects(course.Id);
            return View("Index", model);
        }

    }
}