using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class TeacherProjectStudentViewModel
    {
        public string studentName { get; set; }
        public string projectName { get; set; }
        public string description { get; set; }
        public List<SubmissionViewModel> parts { get; set; }

    }
}