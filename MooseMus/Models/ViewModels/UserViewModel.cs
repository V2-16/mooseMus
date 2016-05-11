using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MooseMus.Models.ViewModels
{
    public class UserViewModel
    {
        [Required (ErrorMessage ="Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }
        [Required(ErrorMessage = "SSN is required")]
        public string ssn { get; set; }
        public int userID { get; set; }


    }
}