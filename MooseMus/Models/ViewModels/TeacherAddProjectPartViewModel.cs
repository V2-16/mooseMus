using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MooseMus.Models.ViewModels
{
    public class TeacherAddProjectPartViewModel
    {
        public int ID { get; set; }
        public int courseID { get; set; }
        public List<ProjectViewModel> projects { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string partName { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string partDescription { get; set; }
        public int projectID { get; set; }
        [Required(ErrorMessage = "Input is required")]
        public string input { get; set; }
        [Required(ErrorMessage = "Output is required")]
        public string output { get; set; }
        public int value { get; set; }

    }
}