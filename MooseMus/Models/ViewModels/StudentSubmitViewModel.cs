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
        [DataType(DataType.Upload)]
        public HttpPostedFileBase fileUploaded { get; set; }
        [Required(ErrorMessage = "Code must be submitted")]
        public string description { get; set; }
        public bool accepted { get; set; }
        public List<String> output { get; set; }
    }
}