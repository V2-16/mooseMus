using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.Entities
{
    public class EnrolledCourseModel
    {
        public List<UserModel> enrolledUsers { get; set; }
        public List<UserModel> unEnrolledUsers { get; set; }
    }
}