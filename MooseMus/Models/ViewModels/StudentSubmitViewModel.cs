using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class StudentSubmitViewModel
    {
        public int studentID { get; set; }
        public int projectPartID { get; set; }
        public string projectPartName { get; set; }
        public HttpPostedFileBase file { get; set; }
    }
}