using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class TeacherAddEditViewModel
    {
        public int courseID { get; set; }
        public int projectID { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string title { get; set; }
        public string projectDescription { get; set; }
        public DateTime deadline { get; set; }
        public bool created { get; set; }
    }
}