using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MooseMus.Models.ViewModels
{
    public class TeacherProjectStudentViewModel
    {
        [Required(ErrorMessage ="Name is required")]
        public string studentName { get; set; }
        [Required(ErrorMessage ="Projectname is required")]
        public string projectName { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string description { get; set; }
        public List<SubmissionViewModel> parts { get; set; }

    }
}