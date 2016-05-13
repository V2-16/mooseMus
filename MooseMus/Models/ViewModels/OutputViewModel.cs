using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class OutputViewModel
    {
        public bool accepted { get; set; }
        public List<String> outputObtained { get; set; }
        public List<String> outputExpected { get; set; }
    }
}