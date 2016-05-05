using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.Entities
{
    public class ResultModel
    {
        public int ID { get; set; }
        public int projectPartID { get; set; }
        public int studentID { get; set; }
        public string result { get; set; }
        public bool accepted { get; set; }
        public bool bestResult { get; set; }
    }
}