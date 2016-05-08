using MooseMus.Models.ViewModels;
using MooseMus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MooseMus.Models.Entities;

namespace MooseMus.Controllers
{
    public class AdminController : Controller
    {
        private UserService _service = new UserService();
        private CourseService _courseService = new CourseService();

        // GET: Admin
        public ActionResult Index(AdminFrontPageViewModel user)
        {
            if (user == null)
            {
                return View();
            }
            else
            {
                var userID1 = _service.getUserIDByPassword(user.password);
                var userID2 = _service.getUserIDByUserName(user.userName);
                if (userID1.Equals(userID2))
                {
                    return View();
                }
                return View();
            }
        }

        [HttpGet]
        public ActionResult addUser()
        {
            return PartialView("Partial/addUser");
        }

        [HttpPost]
        public ActionResult addUser(AddUserViewModel User)
        {
            _service.addUserByID(User);
            return View("Index");
        }

        public ActionResult editUser()
        {
            return PartialView("Partial/editUser");
        }

        [HttpGet]
        public ActionResult searchCourse()
        {
            return PartialView("Partial/searchCourse");
        }

        [HttpGet]
        public ActionResult addCourse()
        {
            return PartialView("Partial/addCourse");
        }

        [HttpPost]
        public ActionResult addCourse(AddCourseViewModel Course)
        {
            _courseService.addCourseByID(Course);
            return View("Index");
        }

        [HttpPost]
        public ActionResult searchCourse(AddCourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                var courseID = _courseService.getCourseIDByCourseName(course.name);
                if ( courseID != 0) 
                {
                    var model = _courseService.getCourseByID(courseID);
                    return View("Partial/editCourse", model);
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult editCourse()
        {
            return PartialView("Partial/editCourse");
        }

        [HttpPost]
        public ActionResult editCourse(AddCourseViewModel Course)
        {
            if (ModelState.IsValid)
            {
               _courseService.updateCourseByID(Course);
                return View("Index");
            }

            return View("Index");
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

        public ActionResult login()
        {
            return View();
        }
    }
}