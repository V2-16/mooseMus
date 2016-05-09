using MooseMus.Models.ViewModels;
using MooseMus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooseMus.Controllers
{
    public class HomeController : Controller
    {
        private UserService _service = new UserService();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FrontPageViewModel user)
        {
            if (ModelState.IsValid)
            {
                var userID1 = _service.getUserIDByUserName(user.userName);
                var userID2 = _service.getUserIDByPasswordAndConfirm(user.password, userID1);
                if (userID2 != 0) //Athuga hvort password og notendanafn stemmi
                {
                    var model = _service.getUserByID(userID1);
                    return View("Login", model);
                }
            }
            
            return View();
        }

        [HttpPost]
        public JsonResult nameInDatabase(string userName)
        {
            var user = _service.getUserIDByUserName(userName);
            return Json(user > 0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login(UserHomeViewModel user)
        {
            return View(user);
        }

        public ActionResult CourseChoose(int userID, string course)
        {
            var user = _service.teacherOrStudent(userID, course);
            if(user == "teacher")
            {
                return RedirectToAction("Index", "Teacher", new { course = course });
            }
            return RedirectToAction("Index", "Student", course);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}