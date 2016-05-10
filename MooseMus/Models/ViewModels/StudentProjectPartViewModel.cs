using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class StudentProjectPartViewModel
    {
        public string partName { get; set; }
        public int projectPartID { get; set; }
        public string studentName { get; set; }
        public List<SubmissionViewModel> submissions { get; set; }
    }
}