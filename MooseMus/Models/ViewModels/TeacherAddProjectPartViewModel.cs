﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class TeacherAddProjectPartViewModel
    {
        public int courseID { get; set; }
        public List<ProjectViewModel> projects { get; set; }
        public string partName { get; set; }
        public string partDescription { get; set; }
        public int projectName { get; set; }
        public string input { get; set; }
        public string output { get; set; }
        public int value { get; set; }

    }
}