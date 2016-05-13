using MooseMus.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooseMus.Models.ViewModels
{
    public class TeacherCourseViewModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public List<CourseModel> courses { get; set; }
        public List<UserModel> users { get; set; }
        public IEnumerable<SelectListItem> userNames
        {
            get { return new SelectList(users, "ID", "name"); }
        }
        public IEnumerable<SelectListItem> courseNames
        {
            get { return new SelectList(courses, "ID", "name"); }
        }
    }
}