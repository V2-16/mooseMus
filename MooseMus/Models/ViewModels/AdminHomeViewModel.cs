using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MooseMus.Models.ViewModels
{
    public class AdminHomeViewModel
    {
        [Required(ErrorMessage ="Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }
        public List<TeacherProjectViewModel> courses { get; set; }
        public List<UserViewModel> users { get; set; }

    }
}