using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.Entities
{
    public class CourseUserModel
    {
        public int ID { get; set; }
        public int userID { get; set; }
        public int courseID { get; set; }
        public string role { get; set; }
    }
}