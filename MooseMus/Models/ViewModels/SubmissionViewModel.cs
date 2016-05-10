using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class SubmissionViewModel
    {
        public int studentID { get; set; }
        public int submission { get; set; }
        public bool accepted { get; set; }
        public bool best { get; set; }
        public string title { get; set; }
        public int projParID { get; set; }
        public int value { get; set; }

    }
}