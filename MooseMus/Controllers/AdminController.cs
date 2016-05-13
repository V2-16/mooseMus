using MooseMus.Models.ViewModels;
using MooseMus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MooseMus.Handlers;
using MooseMus.Models.Entities;

namespace MooseMus.Controllers
{
    [CustomHandleError]
    public class AdminController : Controller
    {
        private UserService _userService = new UserService(null);
        private CourseService _courseService = new CourseService(null);

        // GET: Admin
        public ActionResult Index(AdminFrontPageViewModel user)
        {
            if (user == null)
            {
                return View();
            }
            else
            {
                var userID1 = _userService.getUserIDByPassword(user.password);
                var userID2 = _userService.getUserIDByUserName(user.userName);
                if (userID1.Equals(userID2) && userID1 == 3)
                {
                    System.Web.HttpContext.Current.Session["is_admin"] = true;
                    return View("Home");
                }
                else
                {
                    System.Web.HttpContext.Current.Session["is_admin"] = false;
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult addUser()
        {
            if (System.Web.HttpContext.Current.Session["is_admin"].Equals(true))
            {
                return PartialView("Partial/addUser");
            }
            return View();
        }

        [HttpPost]
        public ActionResult addUser(AddUserViewModel User)
        {
            _userService.addUserByID(User);
            return View("Index");
        }

        [HttpGet]
        public ActionResult editUser()
        {
            return PartialView("Partial/editUser");
        }

        [HttpPost]
        public ActionResult editUser(AddUserViewModel User)
        {
            if (ModelState.IsValid)
            {
                _userService.updateUserByID(User);
                return View("Index");
            }
            return View("Index");
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

        [HttpGet]
        public ActionResult searchUser()
        {
            return PartialView("Partial/searchUser");
        }

        [HttpPost]
        public ActionResult searchUser(AddUserViewModel user)
        {
            if (user.ssn != null)
            {
                var userID = _userService.getUserIDByUserSSN(user.ssn);
                if (userID != 0)
                {
                    var model = _userService.getAddUserViewModelByID(userID);
                    return View("Partial/editUser", model);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult searchCourse()
        {
            return PartialView("Partial/searchCourse");
        }

        [HttpPost]
        public ActionResult searchCourse(AddCourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                var courseID = _courseService.getCourseIDByName(course.name);
                if ( courseID != 0) 
                {
                    var model = _courseService.getCourseByID(courseID);
                    return View("Partial/editCourse", model);
                }
            }
            return View();
        }

        //Admin tengir nemanda við námskeið
        public ActionResult addStudent()
        {
            return View();
        }

        [HttpGet]
        //Admin tengir kennara við námskeið
        public ActionResult addTeacher()
        {
            var courseList = _courseService.getAllCourses();
            var userList = _userService.getAllUsers();

            TeacherCourseViewModel model = new TeacherCourseViewModel { courses = courseList, users = userList  };
            return PartialView("Partial/addTeacher", model);
        }

        public ActionResult linkUser()
        {
            var courseList = _courseService.getAllCourses();

            CourseUsersViewModel model = new CourseUsersViewModel { courses = courseList };
            return PartialView("Partial/linkUser", model);
        }

        [HttpPost]
        public ActionResult linkUser(CourseUsersViewModel model)
        {
            var courseUsersModel = _userService.usersAssociated(model);
            return PartialView("Partial/addUserToCourse", courseUsersModel);
        }

        [HttpPost]
        public ActionResult addUserToCourse(CourseUsersViewModel model)
        {
            _courseService.addUserToCourse(model);
            return PartialView("Index");
        }

        public ActionResult login()
        {
            return View();
        }
    }
}