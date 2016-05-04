using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.Entities
{
    public class ResultModel
    {
        public int submissionID { get; set; }
        public int partID { get; set; }
        public int studentID { get; set; }
        public string result { get; set; }
        public bool accepted { get; set; }
        public bool bestResult { get; set; }
    }
}