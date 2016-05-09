using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.Entities
{
    public class ProjectPartModel
    {
        public int ID { get; set; }
        public int projectID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string input { get; set; }
        public string output { get; set; }
        public int value { get; set; }

    }
}