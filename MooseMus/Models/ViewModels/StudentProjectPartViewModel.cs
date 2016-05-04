using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class StudentProjectPartViewModel
    {
        public string projectName { get; set; }
        public string partName { get; set; }
        public List<SubmissionViewModel> submissions { get; set; }

    }
}