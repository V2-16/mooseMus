﻿using MooseMus.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class TeacherProjectViewModel
    {
        public List<UserViewModel> students { get; set; }
        public string project { get; set; }

    }
}