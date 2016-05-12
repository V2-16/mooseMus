using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MooseMus.Models.ViewModels
{
    public class AdminFrontPageViewModel
    {
        [Required(ErrorMessage ="Username is required")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }
}