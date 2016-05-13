using MooseMus.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MooseMus.Models.ViewModels
{
    public class CourseUsersViewModel
    {
        public int userID { get; set; }
        public int courseID { get; set; }
        public string role { get; set; }
        public List<UserModel> users { get; set; }
        public List<CourseModel> courses { get; set; }
        public List<UserModel> teachers { get; set; }
        public List<UserModel> students { get; set; }
        public List<UserModel> unEnrolledUsers { get; set; }
        public IEnumerable<SelectListItem> courseNames
        {
            get { return new SelectList(courses, "ID", "name"); }
        }
        public IEnumerable<SelectListItem> userNames
        {
            get { return new SelectList(unEnrolledUsers, "ID", "name"); }
        }
    }
}