using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MooseMus.Models.ViewModels
{
    public class StudentProjectPartViewModel
    {
        [Required(ErrorMessage ="Projectname is required")]
        public string partName { get; set; }
        public int projectPartID { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string studentName { get; set; }
        public List<SubmissionViewModel> submissions { get; set; }
    }
}