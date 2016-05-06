using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooseMus.Models.ViewModels
{
    public class FrontPageViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [Remote("nameInDatabase", "Home", HttpMethod = "POST", ErrorMessage = "Username does not exist")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }
}