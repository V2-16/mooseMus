using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.Entities
{
    public class CourseModel
    {
        public int courseID { get; set; }
        public string name { get; set; }
        public string semester { get; set; }
        public string school { get; set; }
    }
}