using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MooseMus.Models.ViewModels
{
    public class CourseProjectsViewModel
    {
        public int studentID { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string name { get; set; }
        public List<ProjectViewModel> projects { get; set; }
    }
}