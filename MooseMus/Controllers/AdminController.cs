using MooseMus.Models.ViewModels;
using MooseMus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooseMus.Controllers
{
    public class AdminController : Controller
    {
        private UserService _service = new UserService();
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
            return View();
        }

        public ActionResult addCourse()
        {
            return View("Partial/addCourse");
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

        public ActionResult login()
        {
            return View();
        }
    }
}