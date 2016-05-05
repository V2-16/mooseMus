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

        public ActionResult Index()
        {
            return View();
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

        public ActionResult login(FrontPageViewModel user)
        {
            if(user == null)
            {
                return View();
            }
            else
            {
                List < CourseViewModel > course = new List<CourseViewModel>
                {
                    new CourseViewModel {name = "course"}
                };

                var model = new UserHomeViewModel
                {
                    userID = 0,
                    name = "maria",
                    email = "maria",
                    courses = course

                };
                var userID1 = _service.getUserIDByPassword(user.password);
                var userID2 = _service.getUserIDByUserName(user.userName);
                if (userID1.Equals(userID2))
                {
                    return View(model);
                }
                return View();
            }
           
        }

        public ActionResult selectCourse()
        {
            return View();
        }
    }
}