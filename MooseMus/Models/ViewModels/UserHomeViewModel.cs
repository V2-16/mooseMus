using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class UserHomeViewModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public List<CourseViewModel> courses { get; set; }
    }
}