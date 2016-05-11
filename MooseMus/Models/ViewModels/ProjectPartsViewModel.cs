using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class ProjectPartsViewModel
    {
        public int projectID { get; set; }
        public string projectName { get; set; }
        public List<ProjectPartsListViewModel> parts { get; set; }

    }
}