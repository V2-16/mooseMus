using MooseMus.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooseMus.Models.ViewModels
{
    public class CourseUsersViewModel
    {
        public int userID { get; set; }
        public int courseID { get; set; }
        public string role { get; set; }
        public List<UserModel> users { get; set; }
        public List<CourseModel> courses { get; set; }
        public IEnumerable<SelectListItem> courseNames
        {
            get { return new SelectList(courses, "ID", "name"); }
        }

    }
}