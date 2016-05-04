using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class AdminHomeViewModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public List<TeacherProjectViewModel> courses { get; set; }
        public List<UserViewModel> users { get; set; }

    }
}