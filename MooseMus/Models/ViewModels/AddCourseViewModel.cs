using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MooseMus.Models.ViewModels
{
    public class AddCourseViewModel
    {
        public int courseID { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Semester is required")]
        public string semester { get; set; }
        [Required(ErrorMessage = "School is required")]
        public string school { get; set; }
    }
}