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
                var userID1 = _service.getUserIDByPassword(user.password);
                var userID2 = _service.getUserIDByUserName(user.userName);
                if (userID1.Equals(userID2) && userID1 != 0) //Athuga hvort password og notendanafn stemmi
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