using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooseMus.Models.Entities
{
    public class EnrolledCourseModel
    {
        public int courseID { get; set; }
        public int userID { get; set; }
        public List<UserModel> teachers {get; set;}
        public List<UserModel> enrolledStudents { get; set; }
        public List<UserModel> unEnrolledUsers { get; set; }
        public IEnumerable<SelectListItem> userNames
        {
            get { return new SelectList(unEnrolledUsers, "ID", "name"); }
        }
        public string role { get; set; }
    }
}