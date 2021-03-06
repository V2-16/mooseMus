﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MooseMus.Models.ViewModels
{
    public class ProjectPartsViewModel
    {
        public int studentID { get; set; }
        public int projectID { get; set; }
        [Required (ErrorMessage ="Projectname is required")]
        public string projectName { get; set; }
        public List<ProjectPartsListViewModel> parts { get; set; }

    }
}