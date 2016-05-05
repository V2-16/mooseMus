﻿using MooseMus.Models.ViewModels;
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult addUser()
        {
            return View();
        }

        public ActionResult editUser()
        {
            return View();
        }

        public ActionResult addCourse()
        {
            return View();
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

        public ActionResult login(AdminFrontPageViewModel user)
        {
            if(user == null)
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
    }
}