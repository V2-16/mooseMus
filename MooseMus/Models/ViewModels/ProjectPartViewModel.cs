using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MooseMus.Models.ViewModels
{
    public class ProjectPartViewModel
    {
        [Required(ErrorMessage ="Name is required")]
        public string name { get; set; }
        public bool accepted { get; set; }
    }
}