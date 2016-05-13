using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class OneSubmissionViewModel
    {
        public bool accepted { get; set; }
        public bool best { get; set; }
        public string title { get; set; }
        public List<String> outputObtained { get; set; }
        public List<String> outputExpected { get; set; }
    }
}