using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class CourseProjectsViewModel
    {
       public string name { get; set; }
       public List<ProjectViewModel> projects { get; set; }
    }
}