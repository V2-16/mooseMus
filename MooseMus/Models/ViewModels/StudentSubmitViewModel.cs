using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class StudentSubmitViewModel
    {
        public int studentID { get; set; }
        public int projectPartID { get; set; }
        public string projectPartName { get; set; }
        [Required(ErrorMessage = "A file upload is required")]
        public HttpPostedFileBase file { get; set; }
    }
}