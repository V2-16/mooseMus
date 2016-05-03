using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.Entities
{
    public class Project
    {
        public int ID { get; set; }
        public int CourseID { get; set; }
        public int Title { get; set; }
    }
}