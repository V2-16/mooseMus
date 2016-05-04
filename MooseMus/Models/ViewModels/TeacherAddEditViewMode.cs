using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class TeacherAddEditViewMode
    {
        public int courseID { get; set; }
        public int projectID { get; set; }
        public int partID { get; set; }
        public string title { get; set; }
        public string courseDescription { get; set; }
        public string partDescription { get; set; }
        public string input { get; set; }
        public string outpu { get; set; }

    }
}